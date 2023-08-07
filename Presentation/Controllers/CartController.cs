using BusinessLogic.DTOs.CartDTOs;
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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IValidator<CartCreateDTO> _cartCreateDtoValidator;
        private readonly IValidator<CartUpdateDTO> _cartUpdateDtoValidator;

        public CartController(ICartService cartService, IValidator<CartCreateDTO> cartCreateDtoValidator, IValidator<CartUpdateDTO> cartUpdateDtoValidator)
        {
            _cartService = cartService;
            _cartCreateDtoValidator = cartCreateDtoValidator;
            _cartUpdateDtoValidator = cartUpdateDtoValidator;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IEnumerable<Cart>>> ReadAsync()
        {
            var data = await _cartService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Cart>> ReadByIdAsync(int id)
        {
            var data = await _cartService.GetByIdAsync<Cart>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateAsync(CartCreateDTO dto)
        {
            var validationResult = await _cartCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartService.CreateAsync(dto);

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
        public async Task<ActionResult<Cart>> UpdateAsync(CartUpdateDTO dto)
        {
            var validationResult = await _cartUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartService.UpdateAsync(dto);
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
            var data = await _cartService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
