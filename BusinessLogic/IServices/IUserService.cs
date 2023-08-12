using Models;
using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices.Base;

namespace BusinessLogic.IServices
{
    public interface IUserService : IService<UserCreateDTO, UserReadAllDTO, UserUpdateDTO, User>
    {
        Task<string> GenerateToken(UserLoginDTO dto);
        Task<UserLoginDTO> AuthenticateUser(UserLoginDTO dto);
        Task CartAssign(UserLoginDTO dto);
        Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);
    }
}
