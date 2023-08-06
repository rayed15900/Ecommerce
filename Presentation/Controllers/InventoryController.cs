using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.IServices;
using FluentValidation;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> ReadAsync()
        {
            var data = await _inventoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> ReadByIdAsync(int id)
        {
            var data = await _inventoryService.GetByIdAsync<Inventory>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Inventory>> CreateAsync(InventoryCreateDTO dto)
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Inventory>> UpdateAsync(InventoryUpdateDTO dto)
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _inventoryService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
