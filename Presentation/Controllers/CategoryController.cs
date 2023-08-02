using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.IServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly ICategoryService _categoryService;
        //public CategoryController(ICategoryService categoryService)
        //{
        //    _categoryService = categoryService;
        //}
        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateAsync(CategoryCreateDTO c)
        //{
        //    try
        //    {
        //        var category = await _categoryService.CreateAsync(c);
        //        if (category != null)
        //        {
        //            return Ok(new { Msg = "Created", Data = category });
        //        }
        //        else
        //        {
        //            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Created", Data = category });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = (Category)null });
        //    }
        //}
    }
}
