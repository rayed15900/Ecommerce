//using Models;
//using MapsterMapper;
//using DataAccess.UnitOfWork;
//using BusinessLogic.IServices;
//using BusinessLogic.Services.Base;
//using BusinessLogic.DTOs.OrderDTOs;
//using BusinessLogic.DTOs.CartDTOs;

//namespace BusinessLogic.Services
//{
//    public class OrderService : Service<OrderCreateDTO, OrderReadAllDTO, OrderUpdateDTO, Order>, IOrderService
//    {
//        private readonly IMapper _mapper;
//        private readonly IUOW _uow;

//        public OrderService(IMapper mapper, IUOW uow) : base(mapper, uow)
//        {
//            _mapper = mapper;
//            _uow = uow;
//        }

//        public async Task<OrderReadByIdDTO> OrderReadByIdAsync(int id)
//        {
//            var orderData = await _uow.GetRepository<Order>().ReadByIdAsync(id);

//            var dto = new OrderReadByIdDTO
//            {
//                Id = id,
//                Username = orderData.Order_User.Username,
//                TotalAmount = orderData.Order_Payment.Amount,
//                Address = orderData.Order_ShippingDetail.Address,
//                OrderStatus = orderData.Status,
//                PaymentStatus = orderData.Order_Payment.Status,
//                OrderItems = orderData.OrderItems
//            };

//            return dto;
//    }

//        public async Task<bool> PlaceOrderAsync(int userId)
//        {
//            if (userId != 0)
//            {
//                var cart = _uow.GetRepository<Cart>().ReadAll().ToList();

//                foreach (var item in cart)
//                {
//                    if (item.UserId == userId)
//                    {
//                        var cartData = await _uow.GetRepository<Cart>().ReadByIdAsync(item.Id);

//                        Payment payment = new Payment()
//                        {
//                            Amount = cartData.TotalAmount,
//                            Status = "Not Paid",
//                            Type = "N/A"
//                        };

//                        var pay = await _uow.GetRepository<Payment>().CreateAsync(payment);
//                        await _uow.SaveChangesAsync();

//                        var shippingDetailList = _uow.GetRepository<ShippingDetail>().ReadAll().ToList();

//                        int sid = 0;

//                        foreach (var shippingDetail in shippingDetailList)
//                        {
//                            if (shippingDetail.UserId == userId)
//                            {
//                                sid = shippingDetail.Id;
//                            }
//                        }

//                        if (sid == 0)
//                        {
//                            return false;
//                        }

//                        Order order = new Order()
//                        {
//                            PaymentId = pay.Id,
//                            UserId = userId,
//                            ShippingDetailId = sid,
//                            Status = "Processing"
//                        };

//                        var orderData = await _uow.GetRepository<Order>().CreateAsync(order);
//                        await _uow.SaveChangesAsync();

//                        var cartItemList = _uow.GetRepository<CartItem>().ReadAll().ToList();

//                        foreach (var cartItem in cartItemList)
//                        {
//                            if(cartItem.CartId == cartData.Id)
//                            {
//                                OrderItem orderItem = new OrderItem()
//                                {
//                                    OrderId = orderData.Id,
//                                    ProductId = cartItem.ProductId,
//                                    Quantity = cartItem.Quantity
//                                };

//                                var productData = await _uow.GetRepository<Product>().ReadByIdAsync(cartItem.ProductId);
//                                var oldInventoryData = await _uow.GetRepository<Inventory>().ReadByIdAsync(productData.InventoryId);

//                                var newInventoryData = oldInventoryData;

//                                newInventoryData.Quantity -= cartItem.Quantity;

//                                _uow.GetRepository<Inventory>().Update(newInventoryData, oldInventoryData);
//                                await _uow.SaveChangesAsync();

//                                await _uow.GetRepository<OrderItem>().CreateAsync(orderItem);
//                                await _uow.SaveChangesAsync();

//                                _uow.GetRepository<CartItem>().Delete(cartItem);
//                                await _uow.SaveChangesAsync();
//                            }
//                        }

//                        _uow.GetRepository<Cart>().Delete(cartData);
//                        await _uow.SaveChangesAsync();

//                        return true;
//                    }
//                }
//            }
//            return false;
//        }
//    }
//}
