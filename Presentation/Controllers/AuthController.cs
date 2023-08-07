using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserCreateDTO> _userCreateDtoValidator;

        public AuthController(IUserService userService, IValidator<UserCreateDTO> userCreateDtoValidator)
        {
            _userService = userService;
            _userCreateDtoValidator = userCreateDtoValidator;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserCreateDTO dto)
        {
            var validationResult = await _userCreateDtoValidator.ValidateAsync(dto);

            if (validationResult.IsValid)
            {
                var data = await _userService.RegisterUserAsync(dto);

                return Ok(new { Msg = "User registered successfully", Data = data });
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(new { Msg = "Validation failed", Errors = ModelState });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            IActionResult response = Unauthorized();
            var _user = await _userService.AuthenticateUser(dto);
            if (_user != null)
            {
                var token = _userService.GenerateToken(dto);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
} 
