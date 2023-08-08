using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Read")]
        public async Task<ActionResult<IEnumerable<User>>> Read()
        {
            var data = await _userService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<User>> ReadById(int id)
        {
            var data = await _userService.GetByIdAsync<User>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //[HttpPost("Update")]
        //public async Task<ActionResult<User>> Update(UserUpdateDTO dto)
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
        //        var errorMessages = new List<string>();
        //        foreach (var error in validationResult.Errors)
        //        {
        //            errorMessages.Add(error.ErrorMessage);
        //        }
        //        return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
        //    }
        //}

        [HttpPost("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _userService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
