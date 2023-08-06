using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using FluentValidation;

namespace BusinessLogic.ValidationRules.UserValidators
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        private readonly IUserService _userService;
        public UserCreateDTOValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _userService.IsEmailUniqueAsync(email);
        }
    }
}
