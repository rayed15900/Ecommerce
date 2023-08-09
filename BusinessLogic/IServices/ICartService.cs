using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface ICartService : IService<CartCreateDTO, CartDetailDTO, CartUpdateDTO, Cart>
    {
        Task<CartDetailDTO> CartDetailAsync();
    }
}
