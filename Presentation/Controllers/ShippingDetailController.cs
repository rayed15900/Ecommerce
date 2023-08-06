using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingDetailController : ControllerBase
    {
        private readonly IShippingDetailService _shippingDetailService;
        private readonly IValidator<ShippingDetailCreateDTO> _shippingDetailCreateDtoValidator;
        private readonly IValidator<ShippingDetailUpdateDTO> _shippingDetailUpdateDtoValidator;

        public ShippingDetailController(IShippingDetailService shippingDetailService, IValidator<ShippingDetailCreateDTO> shippingDetailCreateDtoValidator, IValidator<ShippingDetailUpdateDTO> shippingDetailUpdateDtoValidator)
        {
            _shippingDetailService = shippingDetailService;
            _shippingDetailCreateDtoValidator = shippingDetailCreateDtoValidator;
            _shippingDetailUpdateDtoValidator = shippingDetailUpdateDtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingDetail>>> ReadAsync()
        {
            var data = await _shippingDetailService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingDetail>> ReadByIdAsync(int id)
        {
            var data = await _shippingDetailService.GetByIdAsync<ShippingDetail>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ShippingDetail>> CreateAsync(ShippingDetailCreateDTO dto)
        {
            var validationResult = await _shippingDetailCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _shippingDetailService.CreateAsync(dto);

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
        public async Task<ActionResult<ShippingDetail>> UpdateAsync(ShippingDetailUpdateDTO dto)
        {
            var validationResult = await _shippingDetailUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _shippingDetailService.UpdateAsync(dto);
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
            var data = await _shippingDetailService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
