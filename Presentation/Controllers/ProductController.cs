using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> Create(ProductCreateDTO dto)
        {
            var validationResult = await _productCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _productService.ProductCreateAsync(dto);

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
                var errorMessages = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }
        }

        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> ReadAll()
        {
            var data = await _productService.ReadAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> ReadById(int id)
        {
            var data = await _productService.ProductReadByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Product>> Update(ProductUpdateDTO dto)
        {
            var validationResult = await _productUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _productService.ProductUpdateAsync(dto);
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
                var errorMessages = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _productService.ProductDeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
