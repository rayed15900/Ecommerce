using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices;
using FluentValidation;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class DiscountUpdateDTOValidator : AbstractValidator<DiscountUpdateDTO>
    {
        private readonly IDiscountService _discountService;
        public DiscountUpdateDTOValidator(IDiscountService discountService)
        {
            _discountService = discountService;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
        }
    }
}
