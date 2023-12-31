﻿using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        public DiscountController(
            IDiscountService discountService, 
            IValidator<DiscountCreateDTO> discountCreateDtoValidator, 
            IValidator<DiscountUpdateDTO> discountUpdateDtoValidator)
        {
            _discountService = discountService;
            _discountCreateDtoValidator = discountCreateDtoValidator;
            _discountUpdateDtoValidator = discountUpdateDtoValidator;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Discount>> Create(DiscountCreateDTO dto)
        {
            var validationResult = await _discountCreateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _discountService.CreateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Created", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created" });
        }

        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Discount>>> ReadAll()
        {
            var discounts = await _discountService.ReadAllAsync();
            return Ok(discounts);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Discount>> ReadById(int id)
        {
            var discount = await _discountService.DiscountReadByIdAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Discount>> Update(DiscountUpdateDTO dto)
        {
            var validationResult = await _discountUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _discountService.UpdateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Updated", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated" });
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _discountService.DeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
