using BusinessLogic.DTOs.CategoryDTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.CategoryValidators
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required");
        }
    }
}
