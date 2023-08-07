using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpPost("Create")]
        public async Task<ActionResult<Order>> Create(OrderCreateDTO dto)
        {
            var validationResult = await _orderCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _orderService.CreateAsync(dto);

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

        [HttpPost("Update")]
        public async Task<ActionResult<Order>> Update(OrderUpdateDTO dto)
        {
            var validationResult = await _orderUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _orderService.UpdateAsync(dto);
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
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _orderService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
