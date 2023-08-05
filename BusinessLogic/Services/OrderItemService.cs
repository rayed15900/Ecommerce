using BusinessLogic.DTOs.OrderItemDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class OrderItemService : Service<OrderItemCreateDTO, OrderItemReadDTO, OrderItemUpdateDTO, OrderItem>, IOrderItemService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public OrderItemService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
