using BusinessLogic.DTOs.CategoryDTOs;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryCreateDTO> _categoryCreateDtoValidator;
        private readonly IValidator<CategoryUpdateDTO> _categoryUpdateDtoValidator;

        public CategoryController(ICategoryService categoryService, IValidator<CategoryCreateDTO> categoryCreateDtoValidator, IValidator<CategoryUpdateDTO> categoryUpdateDtoValidator)
        {
            _categoryService = categoryService;
            _categoryCreateDtoValidator = categoryCreateDtoValidator;
            _categoryUpdateDtoValidator = categoryUpdateDtoValidator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Category>>> ReadAsync()
        {
            var data = await _categoryService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> ReadByIdAsync(int id)
        {
            var data = await _categoryService.GetByIdAsync<Category>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateAsync(CategoryCreateDTO dto)
        {
            var validationResult = await _categoryCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _categoryService.CreateAsync(dto);

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
        public async Task<ActionResult<Category>> UpdateAsync(CategoryUpdateDTO dto)
        {
            var validationResult = await _categoryUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _categoryService.UpdateAsync(dto);
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
            var data = await _categoryService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
