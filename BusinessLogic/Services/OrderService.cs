using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class OrderService : Service<OrderCreateDTO, OrderReadDTO, OrderUpdateDTO, Order>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public OrderService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<bool> PlaceOrderAsync(int userId)
        {
            int? cartId = await _uow.GetRepository<Cart>().GetFirstIdAsync();

            if(cartId == null)
            {
                return false;
            }

            var cartData = await _uow.GetRepository<Cart>().GetByIdAsync(cartId);

            var newCartData = cartData;

            newCartData.UserId = userId;

            _uow.GetRepository<Cart>().Update(newCartData, cartData);
            await _uow.SaveChangesAsync();

            Payment payment = new Payment()
            {
                Amount = cartData.TotalAmount,
                Status = "Not Paid"
            };

            var pay = await _uow.GetRepository<Payment>().CreateAsync(payment);
            await _uow.SaveChangesAsync();

            var shippingDetailList = await _uow.GetRepository<ShippingDetail>().GetAllAsync();

            int sid = 0;

            foreach( var shippingDetail in shippingDetailList )
            {
                if(shippingDetail.UserId == userId)
                {
                    sid = shippingDetail.Id;
                }
            }

            if( sid == 0 )
            {
                return false;
            }

            Order order = new Order()
            {
                PaymentId = pay.Id,
                UserId = userId,
                ShippingDetailId = sid,
                Status = "Processing"
            };

            var orderData = await _uow.GetRepository<Order>().CreateAsync(order);
            await _uow.SaveChangesAsync();

            var cartItemList = await _uow.GetRepository<CartItem>().GetAllAsync();

            foreach (var cartItem in cartItemList)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = orderData.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };

                var productData = await _uow.GetRepository<Product>().GetByIdAsync(cartItem.ProductId);
                var oldInventoryData = await _uow.GetRepository<Inventory>().GetByIdAsync(productData.InventoryId);

                var newInventoryData = oldInventoryData;

                newInventoryData.Quantity -= cartItem.Quantity;

                _uow.GetRepository<Inventory>().Update(newInventoryData, oldInventoryData);
                await _uow.SaveChangesAsync();

                await _uow.GetRepository<OrderItem>().CreateAsync(orderItem);
                await _uow.SaveChangesAsync();
            }

            await _uow.GetRepository<Cart>().DeleteAllAsync();
            await _uow.SaveChangesAsync();
            await _uow.GetRepository<CartItem>().DeleteAllAsync();
            await _uow.SaveChangesAsync();

            return true;
        }
    }
}
