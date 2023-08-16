using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.OrderItemDTOs;
using DataAccess.IRepository.Base;

namespace BusinessLogic.Services
{
    public class OrderItemService : Service<OrderItemCreateDTO, OrderItemReadDTO, OrderItemUpdateDTO, OrderItem>, IOrderItemService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<OrderItem> _orderItemRepository;

        public OrderItemService(
            IMapper mapper, 
            IRepository<OrderItem> orderItemRepository) 
            : base(mapper, orderItemRepository)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }
    }
}
