using BusinessLogic.DTOs.InventoryDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.DiscountValidators
{
    public class InventoryUpdateDTOValidator : AbstractValidator<InventoryUpdateDTO>
    {
        public InventoryUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id required");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
        }
    }
}
