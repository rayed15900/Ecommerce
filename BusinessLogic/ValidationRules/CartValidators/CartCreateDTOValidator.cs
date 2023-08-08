using BusinessLogic.DTOs.CartDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartValidators
{
    public class CartCreateDTOValidator : AbstractValidator<CartCreateDTO>
    {
        public CartCreateDTOValidator()
        {
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("TotalAmount required");
        }
    }
}
