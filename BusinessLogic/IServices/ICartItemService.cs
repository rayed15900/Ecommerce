using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.CartItemDTOs;

namespace BusinessLogic.IServices
{
    public interface ICartItemService : IService<CartItemCreateDTO, CartItemReadAllDTO, CartItemUpdateDTO, CartItem>
    {
        Task<CartItemCreateDTO> CartItemCreateAsync(CartItemCreateDTO dto, int userId, string IpAddress);
        Task<CartItemReadByIdDTO> CartItemReadByIdAsync(int id);
        Task<CartItemUpdateDTO> CartItemUpdateAsync(CartItemUpdateDTO dto, int userId, string IpAddress);
        Task<CartItem> CartItemDeleteAsync(int id);
        Task<bool> IsQuantityExceedAsync(int productId, int quantity);
        Task<bool> IsDuplicateProductAsync(int productId);
    }
}
