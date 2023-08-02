using BusinessLogic.DTOs.ProductDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            RuleFor(x => x.StockQuantity)
                .NotEmpty().WithMessage("StockQuantity required");
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId required");
        }
    }
}
