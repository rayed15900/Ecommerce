using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.OrderItemDTOs;

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
