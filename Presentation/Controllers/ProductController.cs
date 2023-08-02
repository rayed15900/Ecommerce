using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
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
        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        public async Task<ActionResult<Product>> CreateAsync(ProductCreateDTO p)
        {
            try
            {
                var product = await _productService.CreateAsync(p);
                if (product != null)
                {
                    return Ok(new { Msg = "Created", Data = product });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = product });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Product)null });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateAsync(ProductUpdateDTO p)
        {
            try
            {
                var product = await _productService.UpdateAsync(p);
                if (product != null)
                {
                    return Ok(new { Msg = "Updated", Data = product });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = product });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Product)null });
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
