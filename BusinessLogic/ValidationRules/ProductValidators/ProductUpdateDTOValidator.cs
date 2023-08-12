using FluentValidation;
using BusinessLogic.DTOs.ProductDTOs;

namespace BusinessLogic.ValidationRules.ProductValidators
{
    public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price required")
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId required");
            RuleFor(x => x.DiscountId)
                .NotEmpty().WithMessage("DiscountId required");
        }
    }
}
