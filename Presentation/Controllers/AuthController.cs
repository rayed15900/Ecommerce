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
        [AllowAnonymous]
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
                var errorMessages = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
                return BadRequest(new { Msg = "Validation failed", Errors = errorMessages });
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginDTO dto)
        {
            var _user = await _userService.AuthenticateUser(dto);
            if (_user != null)
            {
                var token = await _userService.GenerateToken(dto);
                await _userService.CartAssign(dto);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
} 
