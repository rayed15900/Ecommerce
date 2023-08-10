using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices;
using FluentValidation;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class DiscountCreateDTOValidator : AbstractValidator<DiscountCreateDTO>
    {
        private readonly IDiscountService _discountService;
        public DiscountCreateDTOValidator(IDiscountService discountService)
        {
            _discountService = discountService;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required")
                .MustAsync(UniqueName).WithMessage("Name already exists");
        }
        private async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            return await _discountService.IsNameUniqueAsync(name);
        }
    }
}
