﻿using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Services
{
    public class UserService : Service<UserCreateDTO, UserReadDTO, UserUpdateDTO, User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;
        private readonly IConfiguration _config;

        public UserService(IMapper mapper, IUOW uow, IConfiguration config) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
            _config = config;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<string> GenerateToken(UserLoginDTO dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var list = await _uow.GetRepository<User>().GetAllAsync();


            var claims = new List<Claim>();

            foreach (var item in list)
            {
                if (item.Username == dto.Username)
                {
                    if (item.Role == "Customer")
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                    }
                    else if (item.Role == "Admin")
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }
                    claims.Add(new Claim("UserId", item.Id.ToString()));
                }
            }

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public int GetUserIdFromToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        //    var claims = securityToken.Claims;

        //    var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId");

        //    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        //    {
        //        return userId;
        //    }

        //    return -1;
        //}

        public async Task<UserLoginDTO> AuthenticateUser(UserLoginDTO dto)
        {
            var dbUser = await _uow.GetRepository<User>().GetAllAsync();

            foreach (var item in dbUser)
            {
                if (item.Username.Equals(dto.Username))
                {
                    if(VerifyPasswordHash(dto.Password, item.PasswordHash, item.PasswordSalt))
                    {
                        return dto;
                    }
                }
            }
            return null;
        }

        public async Task CartAssign(UserLoginDTO dto)
        {
            var dbUser = await _uow.GetRepository<User>().GetAllAsync();

            foreach (var item in dbUser)
            {
                if (item.Username.Equals(dto.Username))
                {
                    int? cartId = await _uow.GetRepository<Cart>().GetFirstIdAsync();
                    if(cartId == null)
                    {
                        return;
                    }
                    var oldCartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartId);
                    var newCartData = oldCartData;
                    newCartData.UserId = item.Id;
                    _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
                    await _uow.SaveChangesAsync();
                }
            }
        }

        public async Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto)
        {
            var createdEntity = _mapper.Map<User>(dto);

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            createdEntity.Role = "Customer";
            createdEntity.PasswordHash = passwordHash;
            createdEntity.PasswordSalt = passwordSalt;

            await _uow.GetRepository<User>().CreateAsync(createdEntity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<UserCreateDTO>(createdEntity);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var list = await _uow.GetRepository<User>().GetAllAsync();

            foreach(var item in list)
            {
                if (item.Email == email)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var list = await _uow.GetRepository<User>().GetAllAsync();

            foreach (var item in list)
            {
                if (item.Username == username)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
