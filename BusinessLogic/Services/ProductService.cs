using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class ProductService : Service<ProductCreateDTO, ProductReadDTO, ProductUpdateDTO, Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public ProductService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
