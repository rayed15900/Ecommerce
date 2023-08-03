using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly IValidator<CartItemCreateDTO> _cartItemCreateDtoValidator;
        private readonly IValidator<CartItemUpdateDTO> _cartItemUpdateDtoValidator;

        public CartItemController(ICartItemService cartItemService, IValidator<CartItemCreateDTO> cartItemCreateDtoValidator, IValidator<CartItemUpdateDTO> cartItemUpdateDtoValidator)
        {
            _cartItemService = cartItemService;
            _cartItemCreateDtoValidator = cartItemCreateDtoValidator;
            _cartItemUpdateDtoValidator = cartItemUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> ReadAsync()
        {
            var data = await _cartItemService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> ReadByIdAsync(int id)
        {
            var data = await _cartItemService.GetByIdAsync<CartItem>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> CreateAsync(CartItemCreateDTO dto)
        {
            var validationResult = await _cartItemCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartItemService.CreateAsync(dto);

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
        public async Task<ActionResult<CartItem>> UpdateAsync(CartItemUpdateDTO dto)
        {
            var validationResult = await _cartItemUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartItemService.UpdateAsync(dto);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _cartItemService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
