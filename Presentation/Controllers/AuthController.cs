using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using FluentValidation;
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

            //try
            //{
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
            //}
            //catch(Exception ex)
            //{
            //    var innerException = ex.InnerException;
            //    while (innerException != null)
            //    {
            //        if (innerException is Npgsql.PostgresException pgException && pgException.SqlState == "23505")
            //        {
            //            return BadRequest(new { Msg = "Email and Username must be unique" });
            //        }
            //        innerException = innerException.InnerException;
            //    }
            //    return BadRequest(new { Msg = ex.Message });
            //}
        }
    }
}
