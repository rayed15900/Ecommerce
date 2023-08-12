using FluentValidation;
using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.IServices;

namespace BusinessLogic.ValidationRules.CategoryValidators
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        private readonly ICategoryService _categoryService;
        public CategoryUpdateDTOValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id required");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name required")
                .MustAsync(UniqueName).WithMessage("Name already exists");
        }
        private async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            return await _categoryService.IsNameUniqueAsync(name);
        }
    }
}
