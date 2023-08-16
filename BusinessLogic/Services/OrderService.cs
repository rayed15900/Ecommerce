using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.DTOs.CartDTOs;
using DataAccess.IRepository.Base;
using BusinessLogic.IDTOs;
using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.DTOs.OrderItemDTOs;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class OrderService : Service<OrderCreateDTO, OrderReadAllDTO, OrderUpdateDTO, Order>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<ShippingDetail> _shippingDetailRepository;
        private readonly IRepository<Inventory> _inventoryRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderService(
            IMapper mapper, 
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository,
            IRepository<Payment> paymentRepository,
            IRepository<ShippingDetail> shippingDetailRepository,
            IRepository<Inventory> inventoryRepository,
            IRepository<Product> productRepository)
            : base(mapper, orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _paymentRepository = paymentRepository;
            _shippingDetailRepository = shippingDetailRepository;
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderReadByIdDTO> OrderReadByIdAsync(int id)
        {
            var order = await _orderRepository.ReadByIdAsync(id);

            var dto = new OrderReadByIdDTO
            {
                Id = id,
                Username = order.User.Username,
                TotalAmount = order.Payment.Amount,
                Address = order.ShippingDetail.Address,
                OrderStatus = order.Status,
                PaymentStatus = order.Payment.Status,
                OrderItems = order.OrderItems
            };

            return dto;
        }

        public async Task<bool> PlaceOrderAsync(int userId)
        {
            if (userId == 0)
            {
                return false;
            }

            var cart = await _cartRepository.ReadAll().ToListAsync();
            var cartData = cart.FirstOrDefault(item => item.UserId == userId);

            if (cartData == null)
            {
                return false;
            }

            Payment payment = new Payment
            {
                Amount = cartData.TotalAmount,
                Status = "Not Paid",
                Type = "N/A"
            };

            var pay = await _paymentRepository.CreateAsync(payment);

            var shippingDetail = await _shippingDetailRepository.ReadAll().FirstOrDefaultAsync(item => item.UserId == userId);

            if (shippingDetail == null)
            {
                return false;
            }

            Order order = new Order
            {
                PaymentId = pay.Id,
                UserId = userId,
                ShippingDetailId = shippingDetail.Id,
                Status = "Processing"
            };

            var orderData = await _orderRepository.CreateAsync(order);
            var cartItemList = await _cartItemRepository.ReadAll().Where(cartItem => cartItem.CartId == cartData.Id).ToListAsync();

            foreach (var cartItem in cartItemList)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = orderData.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };

                var productData = await _productRepository.ReadByIdAsync(cartItem.ProductId);
                var inventoryData = await _inventoryRepository.ReadByIdAsync(productData.InventoryId);
                inventoryData.Quantity -= cartItem.Quantity;
                await _inventoryRepository.UpdateAsync(inventoryData);

                await _orderItemRepository.CreateAsync(orderItem);
                await _cartItemRepository.DeleteAsync(cartItem);
            }

            await _cartRepository.DeleteAsync(cartData);

            return true;
        }


        //public async Task<bool> PlaceOrderAsync(int userId)
        //{
        //    if (userId != 0)
        //    {
        //        var cart = await _cartRepository.ReadAll().ToListAsync();

        //        foreach (var item in cart)
        //        {
        //            if (item.UserId == userId)
        //            {
        //                var cartData = await _cartRepository.ReadByIdAsync(item.Id);

        //                Payment payment = new Payment()
        //                {
        //                    Amount = cartData.TotalAmount,
        //                    Status = "Not Paid",
        //                    Type = "N/A"
        //                };

        //                var pay = await _paymentRepository.CreateAsync(payment);

        //                var shippingDetailList = await _shippingDetailRepository.ReadAll().ToListAsync();

        //                int sid = 0;

        //                foreach (var shippingDetail in shippingDetailList)
        //                {
        //                    if (shippingDetail.UserId == userId)
        //                    {
        //                        sid = shippingDetail.Id;
        //                    }
        //                }

        //                if (sid == 0)
        //                {
        //                    return false;
        //                }

        //                Order order = new Order()
        //                {
        //                    PaymentId = pay.Id,
        //                    UserId = userId,
        //                    ShippingDetailId = sid,
        //                    Status = "Processing"
        //                };

        //                var orderData = await _orderRepository.CreateAsync(order);

        //                var cartItemList = await _cartItemRepository.ReadAll().ToListAsync();

        //                foreach (var cartItem in cartItemList)
        //                {
        //                    if (cartItem.CartId == cartData.Id)
        //                    {
        //                        OrderItem orderItem = new OrderItem()
        //                        {
        //                            OrderId = orderData.Id,
        //                            ProductId = cartItem.ProductId,
        //                            Quantity = cartItem.Quantity
        //                        };

        //                        var productData = await _productRepository.ReadByIdAsync(cartItem.ProductId);

        //                        var inventoryData = await _inventoryRepository.ReadByIdAsync(productData.InventoryId);
        //                        inventoryData.Quantity -= cartItem.Quantity;
        //                        await _inventoryRepository.UpdateAsync(inventoryData);

        //                        await _orderItemRepository.CreateAsync(orderItem);

        //                        await _cartItemRepository.DeleteAsync(cartItem);
        //                    }
        //                }
        //                await _cartRepository.DeleteAsync(cartData);

        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
