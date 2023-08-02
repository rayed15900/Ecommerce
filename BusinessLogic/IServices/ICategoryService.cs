using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface ICategoryService : IService<CategoryCreateDTO, CategoryReadDTO, CategoryUpdateDTO, Category>
    {
    }
}
