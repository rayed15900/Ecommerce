using BusinessLogic.DTOs.OrderDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.OrderValidators
{
    public class OrderCreateDTOValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidator()
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
