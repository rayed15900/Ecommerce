using BusinessLogic.DTOs.PaymentDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.PaymentValidators
{
    public class PaymentCreateDTOValidator : AbstractValidator<PaymentCreateDTO>
    {
        public PaymentCreateDTOValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Payment status required");
        }
    }
}
