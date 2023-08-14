using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("ReadAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Cart>>> ReadAll()
        {
            string authorizationHeader = Request.Headers["Authorization"];

            string? userIdClaim = null;

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
                userIdClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            }

            if (userIdClaim == null)
            {
                userIdClaim = "0";
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress;

            string ipAddressString = ipAddress?.ToString();

            var data = await _cartService.CartReadAllAsync(Convert.ToInt32(userIdClaim), ipAddressString);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
