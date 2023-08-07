using BusinessLogic.DTOs.DiscountDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class DiscountCreateDTOValidator : AbstractValidator<DiscountCreateDTO>
    {
        public DiscountCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Percent)
                .NotEmpty().WithMessage("Percentage required");
        }
    }
}
