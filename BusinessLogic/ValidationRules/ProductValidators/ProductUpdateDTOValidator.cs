using BusinessLogic.DTOs.ProductDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.ProductValidators
{
    public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price required");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId required");
        }
    }
}
