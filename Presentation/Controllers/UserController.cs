using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDTO> _userCreateDtoValidator;
        private readonly IValidator<UserUpdateDTO> _userUpdateDtoValidator;

        public UserController(IUserService userService, IValidator<UserCreateDTO> userCreateDtoValidator, IValidator<UserUpdateDTO> userUpdateDtoValidator)
        {
            _userService = userService;
            _userCreateDtoValidator = userCreateDtoValidator;
            _userUpdateDtoValidator = userUpdateDtoValidator;
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

        [HttpPost]
        public async Task<ActionResult<User>> CreateAsync(UserCreateDTO dto)
        {
            var validationResult = await _userCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _userService.CreateAsync(dto);

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
        public async Task<ActionResult<User>> UpdateAsync(UserUpdateDTO dto)
        {
            var validationResult = await _userUpdateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _userService.UpdateAsync(dto);
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
            var data = await _userService.RemoveAsync(id);
            if (data == null)
                return NotFound();
            return Ok(new { Msg = "Deleted", Data = data });
        }
    }
}
