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

        public async Task<CartReadAllDTO> CartReadAllAsync(int userId, string ipAddress)
        {
            if (userId != 0)
            {
                var cart = _uow.GetRepository<Cart>().ReadAll().ToList();

                foreach (var item in cart)
                {
                    if (item.UserId == userId)
                    {
                        var cartData = await _uow.GetRepository<Cart>().ReadByIdAsync(item.Id);
                        var cartItemData = _uow.GetRepository<CartItem>().ReadAll().ToList();

                        var cartItems = new List<CartItem>();

                        foreach(var i in cartItemData)
                        {
                            if(i.CartId == cartData.Id)
                            {
                                cartItems.Add(i);
                            }
                        }

                        var dto = new CartReadAllDTO
                        {
                            Id = cartData.Id,
                            TotalAmount = cartData.TotalAmount,
                            CartItems = cartItems,
                            UserId = cartData.UserId
                        };
                        return dto;
                    }
                }
                return null;
            }
            else
            {
                var cart = _uow.GetRepository<Cart>().ReadAll().ToList();

                bool flag = false;

                foreach (var item in cart)
                {
                    if (item.IpAddress == ipAddress)
                    {
                        var cartData = await _uow.GetRepository<Cart>().ReadByIdAsync(item.Id);
                        var cartItemData = _uow.GetRepository<CartItem>().ReadAll().ToList();

                        var cartItems = new List<CartItem>();

                        foreach (var i in cartItemData)
                        {
                            if (i.CartId == cartData.Id)
                            {
                                cartItems.Add(i);
                            }
                        }

                        var dto = new CartReadAllDTO
                        {
                            Id = cartData.Id,
                            TotalAmount = cartData.TotalAmount,
                            CartItems = cartItems,
                            UserId = cartData.UserId
                        };
                        return dto;
                    }
                }
                return null;
            }
        }
    }
}
