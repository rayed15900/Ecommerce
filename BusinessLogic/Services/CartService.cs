using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class CartService : Service<CartCreateDTO, CartReadDTO, CartUpdateDTO, Cart>, ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CartService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
