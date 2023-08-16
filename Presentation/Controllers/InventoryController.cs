using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services;
using BusinessLogic.ValidationRules.DiscountValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IValidator<InventoryUpdateDTO> _inventoryUpdateDtoValidator;

        public InventoryController(
            IInventoryService inventoryService,
            IValidator<InventoryUpdateDTO> inventoryUpdateDtoValidator)
        {
            _inventoryService = inventoryService;
            _inventoryUpdateDtoValidator = inventoryUpdateDtoValidator;
        }

        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Inventory>>> Read()
        {
            var inventories = await _inventoryService.ReadAllAsync();
            return Ok(inventories);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Inventory>> ReadById(int id)
        {
            var inventory = await _inventoryService.InventoryReadByIdAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            return Ok(inventory);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Inventory>> Update(InventoryUpdateDTO dto)
        {
            var validationResult = await _inventoryUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _inventoryService.UpdateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Updated", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated" });
        }
    }
}
