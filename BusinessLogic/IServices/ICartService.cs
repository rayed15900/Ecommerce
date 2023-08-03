using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface ICartService : IService<CartCreateDTO, CartReadDTO, CartUpdateDTO, Cart>
    {
    }
}
