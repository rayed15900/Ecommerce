using BusinessLogic.DTOs.InventoryDTOs;
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
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IValidator<InventoryCreateDTO> _inventoryCreateDtoValidator;
        private readonly IValidator<InventoryUpdateDTO> _inventoryUpdateDtoValidator;

        public InventoryController(IInventoryService inventoryService, IValidator<InventoryCreateDTO> inventoryCreateDtoValidator, IValidator<InventoryUpdateDTO> inventoryUpdateDtoValidator)
        {
            _inventoryService = inventoryService;
            _inventoryCreateDtoValidator = inventoryCreateDtoValidator;
            _inventoryUpdateDtoValidator = inventoryUpdateDtoValidator;
        }

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<Inventory>>> Read()
        {
            var data = await _inventoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<Inventory>> ReadById(int id)
        {
            var data = await _inventoryService.GetByIdAsync<Inventory>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Inventory>> Create(InventoryCreateDTO dto)
        {
            var validationResult = await _inventoryCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _inventoryService.CreateAsync(dto);

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
        public async Task<ActionResult<Inventory>> Update(InventoryUpdateDTO dto)
        {
            var validationResult = await _inventoryUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _inventoryService.UpdateAsync(dto);
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
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _inventoryService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
