﻿using BusinessLogic.DTOs.ProductDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.ProductValidators
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price required");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId required");
            RuleFor(x => x.InventoryId)
                .NotEmpty().WithMessage("InventoryId required");
            RuleFor(x => x.DiscountId)
                .NotEmpty().WithMessage("DiscountId required");
        }
    }
}
