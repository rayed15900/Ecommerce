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

        public CategoryController(
            ICategoryService categoryService, 
            IValidator<CategoryCreateDTO> categoryCreateDtoValidator, 
            IValidator<CategoryUpdateDTO> categoryUpdateDtoValidator)
        {
            _categoryService = categoryService;
            _categoryCreateDtoValidator = categoryCreateDtoValidator;
            _categoryUpdateDtoValidator = categoryUpdateDtoValidator;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> Create(CategoryCreateDTO dto)
        {
            var validationResult = await _categoryCreateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _categoryService.CreateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Created", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created" });
        }


        [HttpGet("ReadAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> ReadAll()
        {
            var categories = await _categoryService.ReadAllAsync();
            return Ok(categories);
        }

        [HttpGet("Read/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> ReadById(int id)
        {
            var category = await _categoryService.CategoryReadByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> Update(CategoryUpdateDTO dto)
        {
            var validationResult = await _categoryUpdateDtoValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }

            var data = await _categoryService.UpdateAsync(dto);

            if (data != null)
            {
                return Ok(new { Msg = "Updated", Data = data });
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Did Not Update" });
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _categoryService.DeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
