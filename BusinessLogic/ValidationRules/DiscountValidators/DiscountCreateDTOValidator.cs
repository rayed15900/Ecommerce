using FluentValidation;
using BusinessLogic.IServices;
using BusinessLogic.DTOs.DiscountDTOs;

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
            RuleFor(x => x.Percent)
                .InclusiveBetween(0, 100).WithMessage("Percent must be between 0 and 100");
        }
        private async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            return await _discountService.IsNameUniqueAsync(name);
        }
    }
}
