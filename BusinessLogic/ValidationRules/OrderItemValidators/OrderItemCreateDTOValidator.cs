using FluentValidation;
using BusinessLogic.DTOs.OrderItemDTOs;

namespace BusinessLogic.ValidationRules.OrderItemValidators
{
    public class OrderItemCreateDTOValidator : AbstractValidator<OrderItemCreateDTO>
    {
        public OrderItemCreateDTOValidator()
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
