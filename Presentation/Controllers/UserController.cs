using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> ReadAsync()
        {
            var data = await _userService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> ReadByIdAsync(int id)
        {
            var data = await _userService.GetByIdAsync<User>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //[HttpPut]
        //public async Task<ActionResult<User>> UpdateAsync(UserUpdateDTO dto)
        //{
        //    var validationResult = await _userUpdateDtoValidator.ValidateAsync(dto);

        //    if (validationResult.IsValid)
        //    {
        //        var data = await _userService.UpdateAsync(dto);
        //        if (data != null)
        //        {
        //            return Ok(new { Msg = "Updated", Data = data });
        //        }
        //        else
        //        {
        //            return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = data });
        //        }
        //    }
        //    else
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, new { Msg = "Invalid input" });
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var data = await _userService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
