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

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Inventory>>> Read()
        {
            var data = await _inventoryService.ReadAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Inventory>> ReadById(int id)
        {
            var data = await _inventoryService.InventoryReadByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
