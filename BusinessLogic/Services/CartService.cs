using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.Services.Base;

namespace BusinessLogic.Services
{
    public class CartService : Service<CartCreateDTO, CartReadAllDTO, CartUpdateDTO, Cart>, ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CartService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }


        public async Task<CartReadAllDTO> CartReadAllAsync()
        {
            var cart = _uow.GetRepository<Cart>().ReadAll().ToList();
            int? cartId = cart.FirstOrDefault()?.Id ?? null;

            if (cartId == null)
            {
                return null;
            }

            var cartData = await _uow.GetRepository<Cart>().ReadByIdAsync(cartId);
            var cartItemData = _uow.GetRepository<CartItem>().ReadAll().ToList();            

            var dto = new CartReadAllDTO
            {
                Id = cartData.Id,
                TotalAmount = cartData.TotalAmount,
                CartItems = cartItemData,
                UserId = cartData.UserId
            };

            return dto;
        }
    }
}
