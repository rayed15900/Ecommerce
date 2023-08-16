using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.CartItemDTOs;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CartItemService : Service<CartItemCreateDTO, CartItemReadAllDTO, CartItemUpdateDTO, CartItem>, ICartItemService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;

        public CartItemService(
            IMapper mapper,
            IRepository<CartItem> cartItemRepository,
            IRepository<Product> productRepository,
            IRepository<Cart> cartRepository)
            : base(mapper, cartItemRepository)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<CartItemCreateDTO> CartItemCreateAsync(CartItemCreateDTO dto, int userId, string ipAddress)
        {
            var cartItem = _mapper.Map<CartItem>(dto);
            var product = await _productRepository.ReadByIdAsync(dto.ProductId);

            double amount = CalculateCartItemAmount(product, dto.Quantity);

            var cart = await _cartRepository.ReadAll().ToListAsync();
            var existingCart = GetCartByUserOrIpAddress(cart, userId, ipAddress);

            if (existingCart != null)
            {
                existingCart.TotalAmount += amount;
                await _cartRepository.UpdateAsync(existingCart);
                cartItem.CartId = existingCart.Id;
            }
            else
            {
                var newCart = new Cart
                {
                    TotalAmount = amount,
                    UserId = userId,
                    IpAddress = ipAddress,
                    IsGuest = userId == 0,
                };

                var createdCart = await _cartRepository.CreateAsync(newCart);
                cartItem.CartId = createdCart.Id;
            }

            var createdCartItem = await _cartItemRepository.CreateAsync(cartItem);
            return _mapper.Map<CartItemCreateDTO>(createdCartItem);
        }

        private double CalculateCartItemAmount(Product product, int quantity)
        {
            if (product.Discount.Active)
            {
                double reducedPrice = product.Price * (product.Discount.Percent / 100);
                return (product.Price - reducedPrice) * quantity;
            }
            return product.Price * quantity;
        }

        private Cart GetCartByUserOrIpAddress(List<Cart> cartList, int userId, string ipAddress)
        {
            foreach (var cart in cartList)
            {
                if ((userId != 0 && cart.UserId == userId) || (userId == 0 && cart.IpAddress == ipAddress))
                {
                    return cart;
                }
            }
            return null;
        }


        //public async Task<CartItemCreateDTO> CartItemCreateAsync(CartItemCreateDTO dto, int userId, string ipAddress)
        //{
        //    var cartItemEntity = _mapper.Map<CartItem>(dto);

        //    var productData = await _productRepository.ReadByIdAsync(dto.ProductId);

        //    double amount;

        //    if (productData.Discount.Active)
        //    {
        //        double discountAmount = productData.Price * (productData.Discount.Percent / 100);
        //        amount = (productData.Price - discountAmount) * dto.Quantity;
        //    }
        //    else
        //    {
        //        amount = productData.Price * dto.Quantity;
        //    }

        //    if (userId != 0)
        //    {
        //        var cart = await _cartRepository.ReadAll().ToListAsync();

        //        bool flag = false;

        //        foreach (var item in cart)
        //        {
        //            if (item.UserId == userId)
        //            {
        //                flag = true;
        //                cartItemEntity.CartId = item.Id;

        //                var cartData = await _cartRepository.ReadByIdAsync(item.Id);

        //                cartData.TotalAmount += amount;

        //                await _cartRepository.UpdateAsync(cartData);

        //                break;
        //            }
        //        }

        //        if (flag == false)
        //        {
        //            var newCart = new Cart()
        //            {
        //                TotalAmount = amount,
        //                UserId = userId,
        //                IpAddress = "",
        //                IsGuest = false
        //            };

        //            var createdCart = await _cartRepository.CreateAsync(newCart);

        //            cartItemEntity.CartId = createdCart.Id;
        //        }
        //    }
        //    else
        //    {
        //        var cart = await _cartRepository.ReadAll().ToListAsync();

        //        bool flag = false;

        //        foreach (var item in cart)
        //        {
        //            if (item.IpAddress == ipAddress)
        //            {
        //                flag = true;
        //                cartItemEntity.CartId = item.Id;

        //                var cartData = await _cartRepository.ReadByIdAsync(item.Id);

        //                cartData.TotalAmount += amount;

        //                await _cartRepository.UpdateAsync(cartData);

        //                break;
        //            }
        //        }

        //        if (flag == false)
        //        {
        //            var newCart = new Cart()
        //            {
        //                TotalAmount = amount,
        //                UserId = 0,
        //                IpAddress = ipAddress,
        //                IsGuest = true
        //            };

        //            var createdCart = await _cartRepository.CreateAsync(newCart);

        //            cartItemEntity.CartId = createdCart.Id;
        //        }
        //    }

        //    var createdCartItem = await _cartItemRepository.CreateAsync(cartItemEntity);

        //    return _mapper.Map<CartItemCreateDTO>(createdCartItem);
        //}

        public async Task<CartItemReadByIdDTO> CartItemReadByIdAsync(int id)
        {
            var cartItem = await _cartItemRepository.ReadByIdAsync(id);

            var dto = new CartItemReadByIdDTO
            {
                Id = id,
                ProductName = cartItem.Product.Name,
                Price = CalculateCartItemAmount(cartItem.Product, cartItem.Quantity),
                Quantity = cartItem.Quantity,
                CartId = cartItem.CartId
            };

            return dto;
        }

        public async Task<CartItemUpdateDTO> CartItemUpdateAsync(CartItemUpdateDTO dto, int userId, string ipAddress)
        {
            await CartItemDeleteAsync(dto.Id);
            var updateDTO = _mapper.Map<CartItemCreateDTO>(dto);
            var newCartItem = await CartItemCreateAsync(updateDTO, userId, ipAddress);
            var newCartItemDTO = _mapper.Map<CartItemUpdateDTO>(newCartItem);
            return newCartItemDTO;

            // Delete old CartItem from Cart

            //var cartItemData = await _cartItemRepository.ReadByIdAsync(dto.Id);
            //var productData = cartItemData.Product;
            //var discountData = productData.Discount;

            //double amount;

            //if (discountData.Active)
            //{
            //    double discountAmount = productData.Price * (discountData.Percent / 100);
            //    amount = (productData.Price - discountAmount) * cartItemData.Quantity;
            //}
            //else
            //{
            //    amount = productData.Price * cartItemData.Quantity;
            //}

            //var cartData = await _cartRepository.ReadByIdAsync(cartItemData.CartId);

            //cartData.TotalAmount -= amount;

            //await _cartRepository.UpdateAsync(cartData);

            // Add new CartItem to Cart

            //var newCartItemData = _mapper.Map<CartItem>(dto);
            //var newProductData = await _productRepository.ReadByIdAsync(dto.ProductId);
            //var newDiscountData = newProductData.Discount;

            //double newAmount;

            //if (newDiscountData.Active)
            //{
            //    double discountAmount = newProductData.Price * (newDiscountData.Percent / 100);
            //    newAmount = (newProductData.Price - discountAmount) * dto.Quantity;
            //}
            //else
            //{
            //    newAmount = newProductData.Price * dto.Quantity;
            //}

            //var newCart = await _cartRepository.ReadByIdAsync(cartItemData.CartId);

            //newCart.TotalAmount += newAmount;

            //await _cartRepository.UpdateAsync(newCart);

            //newCartItemData.CartId = cartItemData.CartId;

            //await _cartItemRepository.UpdateAsync(newCartItemData);

            //var newCartItemDTO = _mapper.Map<CartItemUpdateDTO>(newCartItemData);
            //return newCartItemDTO;


        }

        //private async Task<CartItem> AddCartItem(CartItemUpdateDTO dto)
        //{
        //    var cartItem = _mapper.Map<CartItem>(dto);
        //    var product = await _productRepository.ReadByIdAsync(dto.ProductId);

        //    double amount = CalculateCartItemAmount(product, dto.Quantity);

        //    var cart = cartItem.Cart;

        //    cart.TotalAmount += amount;

        //    await _cartRepository.UpdateAsync(cart);

        //    cartItem.CartId = cart.Id;

        //    await _cartItemRepository.UpdateAsync(cartItem);

        //    return cartItem;
        //}

        public async Task<CartItem> CartItemDeleteAsync(int id)
        {
            var cartItem = await _cartItemRepository.ReadByIdAsync(id);
            var product = cartItem.Product;

            double amount = CalculateCartItemAmount(product, cartItem.Quantity);

            var cart = await _cartRepository.ReadByIdAsync(cartItem.CartId);
            cart.TotalAmount -= amount;
            await _cartRepository.UpdateAsync(cart);

            await _cartItemRepository.DeleteAsync(cartItem);

            return cartItem;
        }

        public async Task<bool> IsQuantityExceedAsync(int productId, int quantity)
        {
            var productData = await _productRepository.ReadByIdAsync(productId);
            return quantity <= productData.Inventory.Quantity;
        }

        public async Task<bool> IsDuplicateProductAsync(int productId)
        {
            bool isUnique = !(await _cartItemRepository.ReadAll().AnyAsync(item => item.ProductId == productId));
            return isUnique;
        }
    }
}
