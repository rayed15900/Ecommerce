using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IOrderService : IService<OrderCreateDTO, OrderReadDTO, OrderUpdateDTO, Order>
    {
    }
}
