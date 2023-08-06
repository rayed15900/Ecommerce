using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IValidator<PaymentCreateDTO> _paymentCreateDtoValidator;
        private readonly IValidator<PaymentUpdateDTO> _paymentUpdateDtoValidator;

        public PaymentController(IPaymentService paymentService, IValidator<PaymentCreateDTO> paymentCreateDtoValidator, IValidator<PaymentUpdateDTO> paymentUpdateDtoValidator)
        {
            _paymentService = paymentService;
            _paymentCreateDtoValidator = paymentCreateDtoValidator;
            _paymentUpdateDtoValidator = paymentUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> ReadAsync()
        {
            var data = await _paymentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> ReadByIdAsync(int id)
        {
            var data = await _paymentService.GetByIdAsync<Payment>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreateAsync(PaymentCreateDTO dto)
        {
            var validationResult = await _paymentCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _paymentService.CreateAsync(dto);

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
        public async Task<ActionResult<Payment>> UpdateAsync(PaymentUpdateDTO dto)
        {
            var validationResult = await _paymentUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _paymentService.UpdateAsync(dto);
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
            var data = await _paymentService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
