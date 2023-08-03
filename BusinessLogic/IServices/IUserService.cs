using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IUserService : IService<UserCreateDTO, UserReadDTO, UserUpdateDTO, User>
    {
    }
}
