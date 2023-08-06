using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;
using System.Security.Cryptography;

namespace BusinessLogic.Services
{
    public class UserService : Service<UserCreateDTO, UserReadDTO, UserUpdateDTO, User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public UserService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<UserCreateDTO> RegisterUserAsync(UserCreateDTO dto)
        {
            var createdEntity = _mapper.Map<User>(dto);

            CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            createdEntity.Username = dto.Username;
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
    }
}
