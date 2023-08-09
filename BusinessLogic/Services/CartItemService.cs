using BusinessLogic.DTOs.CartItemDTOs;
using BusinessLogic.IDTOs;
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

        public async Task<CartItemCreateDTO> CreateCartItemAsync(CartItemCreateDTO dto)
        {
            var cartItemEntity = _mapper.Map<CartItem>(dto);

            var productData = await _uow.GetRepository<Product>().GetByIdAsync(dto.ProductId);
            var discountData = await _uow.GetRepository<Discount>().GetByIdAsync(productData.DiscountId);

            double amount;

            if (discountData.Active)
            {
                double discountAmount = productData.Price * (discountData.Percent / 100);
                amount = (productData.Price - discountAmount) * dto.Quantity;
            }
            else
            {
                amount = productData.Price * dto.Quantity;
            }

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

        public async Task<bool> IsQuantityExceedAsync(int productId, int quantity)
        {
            var productData = await _uow.GetRepository<Product>().GetByIdAsync(productId);
            var inventoryData = await _uow.GetRepository<Inventory>().GetByIdAsync(productData.InventoryId);

            if (quantity <= inventoryData.Quantity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsDuplicateProductAsync(int productId)
        {
            var list = await _uow.GetRepository<CartItem>().GetAllAsync();

            foreach (var item in list)
            {
                if (item.ProductId == productId)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<CartItemUpdateDTO> UpdateCartItemAsync(CartItemUpdateDTO dto)
        {
            // old CartItem

            var cartItemData = await _uow.GetRepository<CartItem>().GetByIdAsync(dto.Id);
            var productData = await _uow.GetRepository<Product>().GetByIdAsync(cartItemData.ProductId);
            var discountData = await _uow.GetRepository<Discount>().GetByIdAsync(productData.DiscountId);

            double amount;

            if (discountData.Active)
            {
                double discountAmount = productData.Price * (discountData.Percent / 100);
                amount = (productData.Price - discountAmount) * cartItemData.Quantity;
            }
            else
            {
                amount = productData.Price * cartItemData.Quantity;
            }

            var oldCartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartItemData.CartId);
            var oldCart = oldCartData;
            var newCartData = oldCartData;

            newCartData.TotalAmount -= amount;

            _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
            await _uow.SaveChangesAsync();

            // new CartItem

            var cartItemEntity = _mapper.Map<CartItem>(dto);
            var newProductData = await _uow.GetRepository<Product>().GetByIdAsync(dto.ProductId);
            var newDiscountData = await _uow.GetRepository<Discount>().GetByIdAsync(newProductData.DiscountId);

            double newAmount;

            if (newDiscountData.Active)
            {
                double discountAmount = newProductData.Price * (newDiscountData.Percent / 100);
                newAmount = (newProductData.Price - discountAmount) * dto.Quantity;
            }
            else
            {
                newAmount = newProductData.Price * dto.Quantity;
            }

            var newCart = oldCart;
            newCart.TotalAmount += newAmount;
            _uow.GetRepository<Cart>().Update(newCart, oldCart);
            await _uow.SaveChangesAsync();


            cartItemEntity.CartId = cartItemData.CartId;
            _uow.GetRepository<CartItem>().Update(cartItemEntity, cartItemData);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CartItemUpdateDTO>(cartItemEntity);
        }

        public async Task<CartItem> DeleteCartItemAsync(int id)
        {
            var cartItemData = await _uow.GetRepository<CartItem>().GetByIdAsync(id);
            var productData = await _uow.GetRepository<Product>().GetByIdAsync(cartItemData.ProductId);
            var discountData = await _uow.GetRepository<Discount>().GetByIdAsync(productData.DiscountId);

            double amount;

            if (discountData.Active)
            {
                double discountAmount = productData.Price * (discountData.Percent / 100);
                amount = (productData.Price - discountAmount) * cartItemData.Quantity;
            }
            else
            {
                amount = productData.Price * cartItemData.Quantity;
            }

            var oldCartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartItemData.CartId);
            var newCartData = oldCartData;

            newCartData.TotalAmount -= amount;

            _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
            await _uow.SaveChangesAsync();

            if (cartItemData != null)
            {
                _uow.GetRepository<CartItem>().Remove(cartItemData);
                await _uow.SaveChangesAsync();
            }
            return cartItemData;
        }
    }
}