using BusinessLogic.DTOs.UserDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

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
    }
}
