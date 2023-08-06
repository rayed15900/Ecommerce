using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IUserService : IService<UserCreateDTO, UserReadDTO, UserUpdateDTO, User>
    {
        Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
