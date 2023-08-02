using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using FluentValidation;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> ReadAsync()
        {
            try
            {
                var data = await _productService.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> ReadByIdAsync(int id)
        {
            try
            {
                var data = await _productService.GetByIdAsync<Product>(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync(ProductCreateDTO dto)
        {
            try
            {
                var validationResult = await _productCreateDtoValidator.ValidateAsync(dto);

                if (validationResult.IsValid)
                {
                    var category = await _productService.CreateAsync(dto);

                    if (category != null)
                    {
                        return Ok(new { Msg = "Created", Data = category });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = category });
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Category)null });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateAsync(ProductUpdateDTO dto)
        {
            try
            {
                var validationResult = await _productUpdateDtoValidator.ValidateAsync(dto);

                if (validationResult.IsValid)
                {
                    var product = await _productService.UpdateAsync(dto);
                    if (product != null)
                    {
                        return Ok(new { Msg = "Updated", Data = product });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = product });
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Category)null });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _productService.RemoveAsync(id);
            if (product == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = product });
        }
    }
}
