﻿using BusinessLogic.DTOs.OrderDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.OrderValidators
{
    public class OrderUpdateDTOValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("Total Amount required");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId required");
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("PaymentId required");
            RuleFor(x => x.ShippingDetailId)
                .NotEmpty().WithMessage("ShippingDetailId required");
        }
    }
}