using BusinessLogic.DTOs.ShippingDetailDTOs;
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

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<ShippingDetail>>> Read()
        {
            var data = await _shippingDetailService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<ShippingDetail>> ReadById(int id)
        {
            var data = await _shippingDetailService.GetByIdAsync<ShippingDetail>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ShippingDetail>> Create(ShippingDetailCreateDTO dto)
        {
            string authorizationHeader = Request.Headers["Authorization"];

            string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            string userIdClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var validationResult = await _shippingDetailCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _shippingDetailService.CreateShippingDetailAsync(dto, Convert.ToInt32(userIdClaim));

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
        public async Task<ActionResult<ShippingDetail>> Update(ShippingDetailUpdateDTO dto)
        {
            var validationResult = await _shippingDetailUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _shippingDetailService.UpdateShippingDetailAsync(dto);
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
            var data = await _shippingDetailService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
