using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.CategoryDTOs;

namespace BusinessLogic.IServices
{
    public interface ICategoryService : IService<CategoryCreateDTO, CategoryReadAllDTO, CategoryUpdateDTO, Category>
    {
        Task<CategoryReadByIdDTO> CategoryReadByIdAsync(int id);
        Task<bool> IsNameUniqueAsync(string name);
    }
}
