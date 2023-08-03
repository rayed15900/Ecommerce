using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ValidationRules.CartValidators
{
    public class CartCreateDTOValidator : AbstractValidator<CartCreateDTO>
    {
        public CartCreateDTOValidator()
        {
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("TotalAmount required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId required");
        }
    }
}
