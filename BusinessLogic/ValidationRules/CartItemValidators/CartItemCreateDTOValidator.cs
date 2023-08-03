using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.CartItemDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ValidationRules.CartItemValidators
{
    public class CartItemCreateDTOValidator : AbstractValidator<CartItemCreateDTO>
    {
        public CartItemCreateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("CartId required");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required");
        }
    }
}
