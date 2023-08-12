using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.OrderDTOs;

namespace BusinessLogic.IServices
{
    public interface IOrderService : IService<OrderCreateDTO, OrderReadAllDTO, OrderUpdateDTO, Order>
    {
        Task<bool> PlaceOrderAsync(int userId);
        Task<OrderReadByIdDTO> OrderReadByIdAsync(int id);
    }
}
