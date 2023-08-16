using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.DTOs.CartDTOs;
using BusinessLogic.Services.Base;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CartService : Service<CartCreateDTO, CartReadAllDTO, CartUpdateDTO, Cart>, ICartService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartService(
            IMapper mapper,
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository) 
            : base(mapper, cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartReadAllDTO> CartReadAllAsync(int userId, string ipAddress)
        {
            var allCarts = await _cartRepository.ReadAll().ToListAsync();
            var matchingCart = allCarts.FirstOrDefault(item => (userId != 0 && item.UserId == userId) || (userId == 0 && item.IpAddress == ipAddress));

            if (matchingCart == null)
            {
                return null;
            }

            var cart = await _cartRepository.ReadByIdAsync(matchingCart.Id);
            var cartItems = await _cartItemRepository.ReadAll().Where(i => i.CartId == cart.Id).ToListAsync();

            var dto = new CartReadAllDTO
            {
                Id = cart.Id,
                IpAddress = cart.IpAddress,
                IsGuest = cart.IsGuest,
                TotalAmount = cart.TotalAmount,
                CartItems = cartItems,
                UserId = cart.UserId ?? 0
            };

            return dto;
        }
    }
}
