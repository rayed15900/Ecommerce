using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.DTOs.ShippingDetailDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ValidationRules.ShippingDetailValidators
{
    public class ShippingDetailCreateDTOValidator : AbstractValidator<ShippingDetailCreateDTO>
    {
        public ShippingDetailCreateDTOValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country required");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City required");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address required");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number required");
        }
    }
}
