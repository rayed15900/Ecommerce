using FluentValidation;
using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.IServices;

namespace BusinessLogic.ValidationRules.CategoryValidators
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        private readonly ICategoryService _categoryService;
        public CategoryCreateDTOValidator(ICategoryService categoryService)
        {
            _categoryService = categoryService;

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
