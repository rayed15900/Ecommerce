using BusinessLogic.DTOs.CartDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CartValidators
{
    public class CartUpdateDTOValidator : AbstractValidator<CartUpdateDTO>
    {
        public CartUpdateDTOValidator()
        {
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("TotalAmount required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId required");
        }
    }
}
