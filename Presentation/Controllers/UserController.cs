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

        [HttpGet("ReadAll")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> ReadAll()
        {
            var data = await _userService.ReadAllAsync();
            return Ok(data);
        }

        [HttpGet("Read/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> ReadById(int id)
        {
            var data = await _userService.ReadByIdAsync<User>(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var data = await _userService.DeleteAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
