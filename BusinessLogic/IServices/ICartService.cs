using Models;
using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.IServices.Base;

namespace BusinessLogic.IServices
{
    public interface ICartService : IService<CartCreateDTO, CartReadAllDTO, CartUpdateDTO, Cart>
    {
        Task<CartReadAllDTO> CartReadAllAsync(int userId, string ipAddress);
    }
}
