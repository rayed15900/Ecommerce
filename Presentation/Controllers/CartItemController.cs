using BusinessLogic.DTOs.CartItemDTOs;
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

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> Create(CartItemCreateDTO dto)
        {
            var validationResult = await _cartItemCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartItemService.CartItemCreateAsync(dto);

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
        public async Task<ActionResult<IEnumerable<CartItem>>> ReadAll()
        {
            var data = await _cartItemService.ReadAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> ReadById(int id)
        {
            var data = await _cartItemService.CartItemReadByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Update")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> Update(CartItemUpdateDTO dto)
        {
            var validationResult = await _cartItemUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _cartItemService.CartItemUpdateAsync(dto);

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
        [AllowAnonymous]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _cartItemService.CartItemDeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
