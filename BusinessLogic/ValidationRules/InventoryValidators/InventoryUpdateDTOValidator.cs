using BusinessLogic.DTOs.InventoryDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.InventoryValidators
{
    public class InventoryUpdateDTOValidator : AbstractValidator<InventoryUpdateDTO>
    {
        public InventoryUpdateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
        }
    }
}
