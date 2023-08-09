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

        public async Task Pay()
        {
            var paymentList = await _uow.GetRepository<Payment>().GetAllAsync();

            foreach(var payment in paymentList)
            {
                if(payment.Status.Equals("Not Paid"))
                {
                    var oldPayment = await _uow.GetRepository<Payment>().GetByIdAsync(payment.Id);
                    var newPayment = oldPayment;

                    newPayment.Status = "Paid";

                    _uow.GetRepository<Payment>().Update(newPayment, payment);
                    await _uow.SaveChangesAsync();
                }
            }

            await _uow.GetRepository<Order>().DeleteAllAsync();
            await _uow.SaveChangesAsync();
            await _uow.GetRepository<OrderItem>().DeleteAllAsync();
            await _uow.SaveChangesAsync();
        }
    }
}