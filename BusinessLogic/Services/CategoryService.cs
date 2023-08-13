using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.CategoryDTOs;

namespace BusinessLogic.Services
{
    public class CategoryService : Service<CategoryCreateDTO, CategoryReadAllDTO, CategoryUpdateDTO, Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CategoryService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CategoryReadByIdDTO> CategoryReadByIdAsync(int id)
        {
            var categoryData = await _uow.GetRepository<Category>().ReadByIdAsync(id);
            
            var productData = _uow.GetRepository<Product>().ReadAll().ToList();

            var productList = new List<CategoryProductReadDTO>();
            
            foreach(var item in productData)
            {
                if(item.CategoryId == id)
                {
                    var productDTO = new CategoryProductReadDTO
                    {
                        ProductId = item.Id,
                        ProductName = item.Name
                    };
                    productList.Add(productDTO);
                }
            }

            var dto = new CategoryReadByIdDTO
            {
                Id = id,
                Name = categoryData.Name,
                Products = productList
            };

            return dto;
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var list = _uow.GetRepository<Category>().ReadAll().ToList();

            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
