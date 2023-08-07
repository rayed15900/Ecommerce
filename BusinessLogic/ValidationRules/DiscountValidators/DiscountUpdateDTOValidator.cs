using BusinessLogic.DTOs.DiscountDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class DiscountUpdateDTOValidator : AbstractValidator<DiscountUpdateDTO>
    {
        public DiscountUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Percent)
                .NotEmpty().WithMessage("Percentage required");
        }
    }
}
