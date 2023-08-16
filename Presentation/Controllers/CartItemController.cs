using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.IdentityModel.Tokens.Jwt;
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

        public CartItemController(
            ICartItemService cartItemService, 
            IValidator<CartItemCreateDTO> cartItemCreateDtoValidator, 
            IValidator<CartItemUpdateDTO> cartItemUpdateDtoValidator)
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

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var userIdClaim = GetUserIdClaimFromToken();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var data = await _cartItemService.CartItemCreateAsync(dto, Convert.ToInt32(userIdClaim), ipAddress);

            if (data != null)
            {
                return Ok(new { Msg = "Created", Data = data });
            }
            
            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created" });
        }

        private string? GetUserIdClaimFromToken()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
                return securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            }
            return null;
        }

        [HttpGet("ReadAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CartItem>>> ReadAll()
        {
            var cartItems = await _cartItemService.ReadAllAsync();
            return Ok(cartItems);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> ReadById(int id)
        {
            var cartItem = await _cartItemService.CartItemReadByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return Ok(cartItem);
        }

        [HttpPost("Update")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> Update(CartItemUpdateDTO dto)
        {
            var validationResult = await _cartItemUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var userIdClaim = GetUserIdClaimFromToken();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var updatedCartItem = await _cartItemService.CartItemUpdateAsync(dto, Convert.ToInt32(userIdClaim), ipAddress);

            if (updatedCartItem != null)
            {
                return Ok(new { Msg = "Updated", Data = updatedCartItem });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated" });
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
