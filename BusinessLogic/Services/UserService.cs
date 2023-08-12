using Models;
using System.Text;
using MapsterMapper;
using DataAccess.UnitOfWork;
using System.Security.Claims;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.UserDTOs;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class UserService : Service<UserCreateDTO, UserReadAllDTO, UserUpdateDTO, User>, IUserService
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

            var list = await _uow.GetRepository<User>().ReadAllAsync();


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

        public async Task<UserLoginDTO> AuthenticateUser(UserLoginDTO dto)
        {
            var dbUser = await _uow.GetRepository<User>().ReadAllAsync();

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
            var userData = await _uow.GetRepository<User>().ReadAllAsync();

            foreach (var item in userData)
            {
                if (item.Username.Equals(dto.Username))
                {
                    var cart = await _uow.GetRepository<Cart>().ReadAllAsync();
                    int? cartId = cart.FirstOrDefault()?.Id ?? null;

                    if (cartId == null)
                    {
                        return;
                    }
                    var oldCartData = await _uow.GetRepository<Cart>().ReadByIdAsync(cartId);
                    var newCartData = oldCartData;

                    newCartData.UserId = item.Id;

                    _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
                    await _uow.SaveChangesAsync();

                    return;
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
            var list = await _uow.GetRepository<User>().ReadAllAsync();

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
            var list = await _uow.GetRepository<User>().ReadAllAsync();

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
