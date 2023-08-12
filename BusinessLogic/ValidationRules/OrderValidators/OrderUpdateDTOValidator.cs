using FluentValidation;
using BusinessLogic.DTOs.OrderDTOs;

namespace BusinessLogic.ValidationRules.OrderValidators
{
    public class OrderUpdateDTOValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId required");
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("PaymentId required");
            RuleFor(x => x.ShippingDetailId)
                .NotEmpty().WithMessage("ShippingDetailId required");
        }
    }
}
