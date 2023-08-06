using BusinessLogic.DTOs.ShippingDetailDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.ShippingDetailValidators
{
    public class ShippingDetailUpdateDTOValidator : AbstractValidator<ShippingDetailUpdateDTO>
    {
        public ShippingDetailUpdateDTOValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country required");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address required");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number required");
        }
    }
}
