using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface ICartItemService : IService<CartItemCreateDTO, CartItemReadDTO, CartItemUpdateDTO, CartItem>
    {
        Task<CartItemCreateDTO> CreateCartItemAsync(CartItemCreateDTO dto);
        Task<bool> IsQuantityExceedAsync(int productId, int quantity);
        Task<bool> IsDuplicateProductAsync(int productId);
        Task<CartItemUpdateDTO> UpdateCartItemAsync(CartItemUpdateDTO dto);
        Task<CartItem> DeleteCartItemAsync(int id);
    }
}
