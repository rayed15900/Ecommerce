using BusinessLogic.DTOs.OrderItemDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IOrderItemService : IService<OrderItemCreateDTO, OrderItemReadDTO, OrderItemUpdateDTO, OrderItem>
    {
    }
}
