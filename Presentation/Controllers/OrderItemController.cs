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

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> Read()
        {
            var data = await _orderItemService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<OrderItem>> ReadById(int id)
        {
            var data = await _orderItemService.GetByIdAsync<OrderItem>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<OrderItem>> Create(OrderItemCreateDTO dto)
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
                var errorMessages = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<OrderItem>> Update(OrderItemUpdateDTO dto)
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
                var errorMessages = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }
        }

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _orderItemService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
