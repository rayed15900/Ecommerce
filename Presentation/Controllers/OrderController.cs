using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Read")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<IEnumerable<Order>>> Read()
        {
            var data = await _orderService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<Order>> ReadById(int id)
        {
            var data = await _orderService.GetByIdAsync<Order>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("PlaceOrder")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> PlaceOrder()
        {
            string authorizationHeader = Request.Headers["Authorization"];

            string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            string userIdClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            bool data = await _orderService.PlaceOrderAsync(Convert.ToInt32(userIdClaim));

            if (data)
            {
                return Ok(new { Msg = "Order placed"});
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Cannot place order" });
            }
        }
    }
}
