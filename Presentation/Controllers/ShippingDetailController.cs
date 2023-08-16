using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        public ShippingDetailController(
            IShippingDetailService shippingDetailService, 
            IValidator<ShippingDetailCreateDTO> shippingDetailCreateDtoValidator, 
            IValidator<ShippingDetailUpdateDTO> shippingDetailUpdateDtoValidator)
        {
            _shippingDetailService = shippingDetailService;
            _shippingDetailCreateDtoValidator = shippingDetailCreateDtoValidator;
            _shippingDetailUpdateDtoValidator = shippingDetailUpdateDtoValidator;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ShippingDetail>> Create(ShippingDetailCreateDTO dto)
        {
            var userIdClaim = GetUserIdClaimFromToken();
            var validationResult = await _shippingDetailCreateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _shippingDetailService.ShippingDetailCreateAsync(dto, Convert.ToInt32(userIdClaim));

            if (data != null)
            {
                return Ok(new { Msg = "Created", Data = data });
            }

            return BadRequest(new { Msg = "Cannot add multiple Shipping Detail" });
        }

        private string? GetUserIdClaimFromToken()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            {
                string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
                return securityToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            }
            return null;
        }

        [HttpGet("ReadAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ShippingDetail>>> ReadAll()
        {
            var shippingDetails = await _shippingDetailService.ReadAllAsync();
            return Ok(shippingDetails);
        }

        [HttpGet("Read/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ShippingDetail>> ReadById(int id)
        {
            var shippingDetail = await _shippingDetailService.ShippingDetailReadByIdAsync(id);
            if (shippingDetail == null)
            {
                return NotFound();
            }
            return Ok(shippingDetail);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<ShippingDetail>> Update(ShippingDetailUpdateDTO dto)
        {
            var validationResult = await _shippingDetailUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _shippingDetailService.ShippingDetailUpdateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Updated", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = data });
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _shippingDetailService.DeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
