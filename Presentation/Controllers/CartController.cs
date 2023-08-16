using BusinessLogic.IDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services;
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Cart>>> ReadAll()
        {
            var userIdClaim = GetUserIdClaimFromToken();

            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var computerName = System.Net.Dns.GetHostEntry(remoteIpAddress).HostName.Split('.')[0];
            var machineName = System.Environment.MachineName;

            string combinedAddress = $"{computerName}-{remoteIpAddress}-{machineName}";

            var cart = await _cartService.CartReadAllAsync(Convert.ToInt32(userIdClaim), combinedAddress);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
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
    }
}
