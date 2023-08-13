using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.CartItemDTOs;

namespace BusinessLogic.Services
{
    public class CartItemService : Service<CartItemCreateDTO, CartItemReadAllDTO, CartItemUpdateDTO, CartItem>, ICartItemService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public CartItemService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CartItemCreateDTO> CartItemCreateAsync(CartItemCreateDTO dto)
        {
            var cartItemEntity = _mapper.Map<CartItem>(dto);

            var productData = await _uow.GetRepository<Product>().ReadByIdAsync(dto.ProductId);

            double amount;
            
            if (productData.Product_Discount.Active)
            {
                double discountAmount = productData.Price * (productData.Product_Discount.Percent / 100);
                amount = (productData.Price - discountAmount) * dto.Quantity;
            }
            else
            {
                amount = productData.Price * dto.Quantity;
            }

            var cart = _uow.GetRepository<Cart>().ReadAll().ToList();
            int? cartId = cart.FirstOrDefault()?.Id ?? null;

            if (cartId == null)
            {
                var newCart = new Cart()
                {
                    TotalAmount = amount
                };
                
                var createdCart = await _uow.GetRepository<Cart>().CreateAsync(newCart);
                await _uow.SaveChangesAsync();

                cartItemEntity.CartId = createdCart.Id;
            }
            else
            {
                cartItemEntity.CartId = (int)cartId;

                var oldCartData = await _uow.GetRepository<Cart>().ReadByIdAsync(cartId);
                var newCartData = oldCartData;

                newCartData.TotalAmount += amount;

                _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
                await _uow.SaveChangesAsync();
            }

            var createdCartItem = await _uow.GetRepository<CartItem>().CreateAsync(cartItemEntity);
            await _uow.SaveChangesAsync();

            return _mapper.Map<CartItemCreateDTO>(createdCartItem);
        }

        public async Task<CartItemReadByIdDTO> CartItemReadByIdAsync(int id)
        {
            var cartItemData = await _uow.GetRepository<CartItem>().ReadByIdAsync(id);

            var dto = new CartItemReadByIdDTO
            {
                Id = id,
                ProductName = cartItemData.CartItem_Product.Name,
                Quantity = cartItemData.Quantity,
                CartId = cartItemData.CartId
            };

            return dto;
        }

        public async Task<CartItemUpdateDTO> CartItemUpdateAsync(CartItemUpdateDTO dto)
        {
            // Delete old CartItem from Cart

            var cartItemData = await _uow.GetRepository<CartItem>().ReadByIdAsync(dto.Id);
            var productData = cartItemData.CartItem_Product;
            var discountData = productData.Product_Discount;

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

            var oldCartData = await _uow.GetRepository<Cart>().ReadByIdAsync(cartItemData.CartId);
            var newCartData = oldCartData;

            newCartData.TotalAmount -= amount;

            _uow.GetRepository<Cart>().Update(newCartData, oldCartData);
            await _uow.SaveChangesAsync();

            // Add new CartItem to Cart

            var newCartItemData = _mapper.Map<CartItem>(dto);
            var newProductData = await _uow.GetRepository<Product>().ReadByIdAsync(dto.ProductId);
            var newDiscountData = newProductData.Product_Discount;

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

            var oldCart = await _uow.GetRepository<Cart>().ReadByIdAsync(cartItemData.CartId);
            var newCart = oldCart;

            newCart.TotalAmount += newAmount;

            _uow.GetRepository<Cart>().Update(newCart, oldCart);
            await _uow.SaveChangesAsync();

            newCartItemData.CartId = cartItemData.CartId;

            _uow.GetRepository<CartItem>().Update(newCartItemData, cartItemData);
            await _uow.SaveChangesAsync();

            return _mapper.Map<CartItemUpdateDTO>(newCartItemData);
        }

        public async Task<CartItem> CartItemDeleteAsync(int id)
        {
            var cartItemData = await _uow.GetRepository<CartItem>().ReadByIdAsync(id);
            var productData = cartItemData.CartItem_Product;
            var discountData = productData.Product_Discount;

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

            var oldCartData = await _uow.GetRepository<Cart>().ReadByIdAsync(cartItemData.CartId);
            var newCartData = oldCartData;

            newCartData.TotalAmount -= amount;

            _uow.GetRepository<Cart>().Update(newCartData, oldCartData);

            _uow.GetRepository<CartItem>().Delete(cartItemData);
            await _uow.SaveChangesAsync();

            return cartItemData;
        }

        public async Task<bool> IsQuantityExceedAsync(int productId, int quantity)
        {
            var productData = await _uow.GetRepository<Product>().ReadByIdAsync(productId);

            if (quantity <= productData.Product_Inventory.Quantity)
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
            var list = _uow.GetRepository<CartItem>().ReadAll().ToList();

            foreach (var item in list)
            {
                if (item.ProductId == productId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}