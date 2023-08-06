using BusinessLogic.DTOs.OrderItemDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IValidator<OrderItemCreateDTO> _orderItemCreateDtoValidator;
        private readonly IValidator<OrderItemUpdateDTO> _orderItemUpdateDtoValidator;

        public OrderItemController(IOrderItemService orderItemService, IValidator<OrderItemCreateDTO> orderItemCreateDtoValidator, IValidator<OrderItemUpdateDTO> orderItemUpdateDtoValidator)
        {
            _orderItemService = orderItemService;
            _orderItemCreateDtoValidator = orderItemCreateDtoValidator;
            _orderItemUpdateDtoValidator = orderItemUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> ReadAsync()
        {
            var data = await _orderItemService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> ReadByIdAsync(int id)
        {
            var data = await _orderItemService.GetByIdAsync<OrderItem>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateAsync(OrderItemCreateDTO dto)
        {
            var validationResult = await _orderItemCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _orderItemService.CreateAsync(dto);

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
        public async Task<ActionResult<OrderItem>> UpdateAsync(OrderItemUpdateDTO dto)
        {
            var validationResult = await _orderItemUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _orderItemService.UpdateAsync(dto);
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
            var data = await _orderItemService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
