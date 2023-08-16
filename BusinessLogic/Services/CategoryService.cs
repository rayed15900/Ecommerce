using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.CategoryDTOs;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CategoryService : Service<CategoryCreateDTO, CategoryReadAllDTO, CategoryUpdateDTO, Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(
            IMapper mapper,
            IRepository<Category> categoryRepository) 
            : base(mapper, categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryReadByIdDTO> CategoryReadByIdAsync(int id)
        {
            var category = await _categoryRepository.ReadByIdAsync(id);

            if (category == null)
            {
                return null;
            }

            var productList = category.Products.Select(item => new CategoryProductReadDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Inventory.Quantity
            }).ToList();

            var dto = new CategoryReadByIdDTO
            {
                Id = id,
                Name = category.Name,
                Products = productList
            };

            return dto;
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            bool isNameUnique = !(await _categoryRepository.ReadAll().AnyAsync(item => item.Name == name));
            return isNameUnique;
        }
    }
}
