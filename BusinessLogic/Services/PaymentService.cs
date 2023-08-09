using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class PaymentService : Service<PaymentCreateDTO, PaymentReadDTO, PaymentUpdateDTO, Payment>, IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public PaymentService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task Pay(int orderId)
        {
            var orderData = await _uow.GetRepository<Order>().GetByIdAsync(orderId);

            var newOrderData = orderData;

            newOrderData.Status = "Complete";

            _uow.GetRepository<Order>().Update(newOrderData, orderData);
            await _uow.SaveChangesAsync();

            var oldPayment = await _uow.GetRepository<Payment>().GetByIdAsync(orderData.PaymentId);
            var newPayment = oldPayment;

            newPayment.Status = "Paid";

            _uow.GetRepository<Payment>().Update(newPayment, oldPayment);
            await _uow.SaveChangesAsync();
        }
    }
}