using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.OrderItemDTOs;

namespace BusinessLogic.IServices
{
    public interface IOrderItemService : IService<OrderItemCreateDTO, OrderItemReadDTO, OrderItemUpdateDTO, OrderItem>
    {
    }
}
