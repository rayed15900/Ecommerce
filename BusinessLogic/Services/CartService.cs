using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class CartService : Service<CartCreateDTO, CartDetailDTO, CartUpdateDTO, Cart>, ICartService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CartService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }


        public async Task<CartDetailDTO> CartDetailAsync()
        {
            int? cartId = await _uow.GetRepository<Cart>().GetFirstIdAsync();

            if (cartId == null)
            {
                return null;
            }

            var cartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartId);
            var cartItemData = await _uow.GetRepository<CartItem>().GetAllAsync();

            

            var dto = new CartDetailDTO
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
