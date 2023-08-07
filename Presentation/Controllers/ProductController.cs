using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<ProductCreateDTO> _productCreateDtoValidator;
        private readonly IValidator<ProductUpdateDTO> _productUpdateDtoValidator;

        public ProductController(IProductService productService, IValidator<ProductCreateDTO> productCreateDtoValidator, IValidator<ProductUpdateDTO> productUpdateDtoValidator)
        {
            _productService = productService;
            _productCreateDtoValidator = productCreateDtoValidator;
            _productUpdateDtoValidator = productUpdateDtoValidator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> ReadAsync()
        {
            var data = await _productService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> ReadByIdAsync(int id)
        {
            var data = await _productService.GetByIdAsync<Product>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> CreateAsync(ProductCreateDTO dto)
        {
            var validationResult = await _productCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _productService.CreateAsync(dto);

                if (data != null)
                {
                    return Ok(new { Msg = "Created", Data = data });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = data });
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> UpdateAsync(ProductUpdateDTO dto)
        {
            var validationResult = await _productUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _productService.UpdateAsync(dto);
                if (data != null)
                {
                    return Ok(new { Msg = "Updated", Data = data });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = data });
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _productService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
