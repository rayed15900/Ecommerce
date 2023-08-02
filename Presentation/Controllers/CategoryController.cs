using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.DTOs.Interfaces;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using FluentValidation;
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
        public async Task<ActionResult<IEnumerable<Category>>> ReadAsync()
        {
            try
            {
                var data = await _categoryService.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> ReadByIdAsync(int id)
        {
            try
            {
                var data = await _categoryService.GetByIdAsync<Category>(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateAsync(CategoryCreateDTO dto)
        {
            try
            {
                var validationResult = await _categoryCreateDtoValidator.ValidateAsync(dto);

                if (validationResult.IsValid)
                {
                    var category = await _categoryService.CreateAsync(dto);

                    if (category != null)
                    {
                        return Ok(new { Msg = "Created", Data = category });
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = category });
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Category)null });
            }
        }
    }
}
