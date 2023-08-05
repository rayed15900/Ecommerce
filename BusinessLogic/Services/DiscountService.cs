using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class DiscountService : Service<DiscountCreateDTO, DiscountReadDTO, DiscountUpdateDTO, Discount>, IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public DiscountService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
