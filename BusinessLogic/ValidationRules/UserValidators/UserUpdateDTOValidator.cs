﻿using FluentValidation;
using BusinessLogic.IServices;
using BusinessLogic.DTOs.UserDTOs;

namespace BusinessLogic.ValidationRules.UserValidators
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        private readonly IUserService _userService;
        public UserUpdateDTOValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Lastname required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email required")
                .EmailAddress().WithMessage("Invalid email address")
                .MustAsync(UniqueEmail).WithMessage("Email already exists");
            RuleFor(x => x.DOB)
                .NotEmpty().WithMessage("DOB required");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long")
                .MustAsync(UniqueUsername).WithMessage("Username already exists");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long");
        }

        private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _userService.IsEmailUniqueAsync(email);
        }
        private async Task<bool> UniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _userService.IsUsernameUniqueAsync(username);
        }
    }
}
