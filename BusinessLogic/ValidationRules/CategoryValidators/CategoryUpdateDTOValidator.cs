using BusinessLogic.DTOs.CategoryDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CategoryValidators
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
        }
    }
}
