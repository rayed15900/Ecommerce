using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services;
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

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<Payment>>> Read()
        {
            var data = await _paymentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<Payment>> ReadById(int id)
        {
            var data = await _paymentService.GetByIdAsync<Payment>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Pay")]
        public async Task<ActionResult> Pay()
        {
            await _paymentService.Pay();

            return Ok(new { Msg = "Payment Successful"});
        }
    }
}
