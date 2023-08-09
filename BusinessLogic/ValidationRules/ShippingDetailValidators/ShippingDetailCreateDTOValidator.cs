using BusinessLogic.DTOs.ShippingDetailDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.ShippingDetailValidators
{
    public class ShippingDetailCreateDTOValidator : AbstractValidator<ShippingDetailCreateDTO>
    {
        public ShippingDetailCreateDTOValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country required");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address required");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number required")
                .Matches("^[0-9]*$").WithMessage("Phone number must contain only digits")
                .Length(11).WithMessage("Phone number must be exactly 11 digits");
        }
    }
}
