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

        [HttpGet("Read")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Payment>>> Read()
        {
            var data = await _paymentService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Payment>> ReadById(int id)
        {
            var data = await _paymentService.GetByIdAsync<Payment>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Pay/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Pay(int id)
        {
            await _paymentService.Pay(id);

            return Ok(new { Msg = "Payment Successful"});
        }
    }
}
