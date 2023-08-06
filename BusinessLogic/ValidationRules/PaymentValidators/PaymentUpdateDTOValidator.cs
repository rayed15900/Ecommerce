using BusinessLogic.DTOs.PaymentDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.PaymentValidators
{
    public class PaymentUpdateDTOValidator : AbstractValidator<PaymentUpdateDTO>
    {
        public PaymentUpdateDTOValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Payment status required");
        }
    }
}
