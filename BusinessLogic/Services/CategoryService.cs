using BusinessLogic.DTOs.CategoryDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class CategoryService : Service<CategoryCreateDTO, CategoryReadDTO, CategoryUpdateDTO, Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CategoryService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
