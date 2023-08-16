using Models;
using System.Text;
using MapsterMapper;
using System.Security.Claims;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.UserDTOs;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class UserService : Service<UserCreateDTO, UserReadAllDTO, UserUpdateDTO, User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IConfiguration _config;

        public UserService(
            IMapper mapper, 
            IRepository<User> userRepository,
            IRepository<Cart> cartRepository,
            IConfiguration config) 
            : base(mapper, userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
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

            var userlist = await _userRepository.ReadAll().ToListAsync();

            var claims = new List<Claim>();

            foreach (var item in userlist)
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
            var user = await _userRepository.ReadAll().FirstOrDefaultAsync(item => item.Username.Equals(dto.Username));

            if (user != null && VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return dto;
            }

            return null;
        }

        public async Task CartAssign(UserLoginDTO dto, string ipAddress)
        {
            var user = await _userRepository.ReadAll().FirstOrDefaultAsync(item => item.Username.Equals(dto.Username));

            if (user != null)
            {
                var cart = await _cartRepository.ReadAll().FirstOrDefaultAsync(i => i.IpAddress.Equals(ipAddress));

                if (cart != null)
                {
                    cart.UserId = user.Id;
                    cart.IpAddress = "";
                    cart.IsGuest = false;

                    await _cartRepository.UpdateAsync(cart);
                }
            }
        }

        public async Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto)
        {
            var user = _mapper.Map<User>(dto);

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Role = "Customer";
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserCreateDTO>(createdUser);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var isEmailUnique = !(await _userRepository.ReadAll().AnyAsync(item => item.Email == email));
            return isEmailUnique;
        }

        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            var isUsernameUnique = !(await _userRepository.ReadAll().AnyAsync(item => item.Username == username));
            return isUsernameUnique;
        }
    }
}
