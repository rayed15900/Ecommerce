using BusinessLogic.DTOs.CartItemDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartItemValidators
{
    public class CartItemCreateDTOValidator : AbstractValidator<CartItemCreateDTO>
    {
        public CartItemCreateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required");
        }
    }
}
