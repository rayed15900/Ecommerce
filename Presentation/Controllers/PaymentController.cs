﻿using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("ReadAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Payment>>> ReadAll()
        {
            var payments = await _paymentService.ReadAllAsync();
            return Ok(payments);
        }

        [HttpPost("Pay/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Pay(int id, PayDTO dto)
        {
            await _paymentService.Pay(id, dto);

            return Ok(new { Msg = "Payment Successful"});
        }
    }
}
