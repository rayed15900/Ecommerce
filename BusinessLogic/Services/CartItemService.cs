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
    public class CartItemService : Service<CartItemCreateDTO, CartItemReadDTO, CartItemUpdateDTO, CartItem>, ICartItemService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CartItemService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CartItemCreateDTO> CreateCartAsync(CartItemCreateDTO dto)
        {
            var cartItemEntity = _mapper.Map<CartItem>(dto);

            var productData = await _uow.GetRepository<Product>().GetByIdAsync(dto.ProductId);

            double amount = productData.Price * dto.Quantity;

            int? cartId = await _uow.GetRepository<Cart>().GetFirstIdAsync();

            if (cartId == null) 
            {
                Cart newCart = new Cart();
                newCart.TotalAmount = amount;
                await _uow.GetRepository<Cart>().CreateAsync(newCart);
                await _uow.SaveChangesAsync();
                cartItemEntity.CartId = (int)await _uow.GetRepository<Cart>().GetFirstIdAsync();
            }
            else
            {
                cartItemEntity.CartId = (int)cartId;
                var oldCartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartId);
                var newCartData = oldCartData;
                newCartData.TotalAmount += amount;
                _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
                await _uow.SaveChangesAsync();
            }

            await _uow.GetRepository<CartItem>().CreateAsync(cartItemEntity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CartItemCreateDTO>(cartItemEntity);
        }
    }
}
