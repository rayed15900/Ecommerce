using BusinessLogic.DTOs.InventoryDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.InventoryValidators
{
    public class InventoryCreateDTOValidator : AbstractValidator<InventoryCreateDTO>
    {
        public InventoryCreateDTOValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity required");
        }
    }
}
