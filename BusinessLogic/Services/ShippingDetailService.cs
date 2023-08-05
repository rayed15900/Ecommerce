using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class ShippingDetailService : Service<ShippingDetailCreateDTO, ShippingDetailReadDTO, ShippingDetailUpdateDTO, ShippingDetail>, IShippingDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public ShippingDetailService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
