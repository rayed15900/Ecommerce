using BusinessLogic.DTOs.OrderItemDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ValidationRules.OrderItemValidators
{
    public class OrderItemUpdateDTOValidator : AbstractValidator<OrderItemUpdateDTO>
    {
        public OrderItemUpdateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId required");
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId required");
        }
    }
}
