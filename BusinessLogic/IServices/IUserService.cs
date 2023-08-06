﻿using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IUserService : IService<UserCreateDTO, UserReadDTO, UserUpdateDTO, User>
    {
        Task<string> GenerateToken();
        Task<UserLoginDTO> AuthenticateUser(UserLoginDTO dto);
        Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsUsernameUniqueAsync(string username);
    }
}
