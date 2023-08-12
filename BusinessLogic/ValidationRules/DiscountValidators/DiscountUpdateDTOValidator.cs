using FluentValidation;
using BusinessLogic.DTOs.DiscountDTOs;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class DiscountUpdateDTOValidator : AbstractValidator<DiscountUpdateDTO>
    {
        public DiscountUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Percent)
                .InclusiveBetween(0, 100).WithMessage("Percent must be between 0 and 100");
        }
    }
}
