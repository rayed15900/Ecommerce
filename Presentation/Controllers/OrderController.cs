using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.IServices;
using FluentValidation;
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
        private readonly IValidator<OrderCreateDTO> _orderCreateDtoValidator;
        private readonly IValidator<OrderUpdateDTO> _orderUpdateDtoValidator;

        public OrderController(IOrderService orderService, IValidator<OrderCreateDTO> orderCreateDtoValidator, IValidator<OrderUpdateDTO> orderUpdateDtoValidator)
        {
            _orderService = orderService;
            _orderCreateDtoValidator = orderCreateDtoValidator;
            _orderUpdateDtoValidator = orderUpdateDtoValidator;
        }

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<Order>>> Read()
        {
            var data = await _orderService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
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
                return Ok(new { Msg = "Placed", Data = data });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Placed", Data = data });
            }
        }
    }
}
