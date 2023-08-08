using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface ICartItemService : IService<CartItemCreateDTO, CartItemReadDTO, CartItemUpdateDTO, CartItem>
    {
        Task<CartItemCreateDTO> CreateCartAsync(CartItemCreateDTO dto);
        //Task<CartItemUpdateDTO> UpdateCartAsync(CartItemUpdateDTO dto);
    }
}
