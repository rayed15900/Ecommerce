using BusinessLogic.DTOs.CartItemDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartItemValidators
{
    public class CartItemUpdateDTOValidator : AbstractValidator<CartItemUpdateDTO>
    {
        public CartItemUpdateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required");
        }
    }
}
