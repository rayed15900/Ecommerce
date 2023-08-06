using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IValidator<DiscountCreateDTO> _discountCreateDtoValidator;
        private readonly IValidator<DiscountUpdateDTO> _discountUpdateDtoValidator;

        public DiscountController(IDiscountService discountService, IValidator<DiscountCreateDTO> discountCreateDtoValidator, IValidator<DiscountUpdateDTO> discountUpdateDtoValidator)
        {
            _discountService = discountService;
            _discountCreateDtoValidator = discountCreateDtoValidator;
            _discountUpdateDtoValidator = discountUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> ReadAsync()
        {
            var data = await _discountService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> ReadByIdAsync(int id)
        {
            var data = await _discountService.GetByIdAsync<Discount>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Discount>> CreateAsync(DiscountCreateDTO dto)
        {
            var validationResult = await _discountCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _discountService.CreateAsync(dto);

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
        public async Task<ActionResult<Discount>> UpdateAsync(DiscountUpdateDTO dto)
        {
            var validationResult = await _discountUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _discountService.UpdateAsync(dto);
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
            var data = await _discountService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
