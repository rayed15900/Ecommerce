using BusinessLogic.DTOs.UserDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.UserValidators
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required");
        }
    }
}
