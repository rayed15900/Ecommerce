using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.PaymentDTOs;
using DataAccess.IRepository.Base;

namespace BusinessLogic.Services
{
    public class PaymentService : Service<PaymentCreateDTO, PaymentReadDTO, PaymentUpdateDTO, Payment>, IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Order> _orderRepository;

        public PaymentService(
            IMapper mapper,
            IRepository<Payment> paymentRepository,
            IRepository<Order> orderRepository) 
            : base(mapper, paymentRepository)
        {
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task Pay(int orderId, PayDTO dto)
        {
            var order = await _orderRepository.ReadByIdAsync(orderId);
            order.Status = "Complete";
            await _orderRepository.UpdateAsync(order);

            var payment = await _paymentRepository.ReadByIdAsync(order.PaymentId);
            payment.Status = "Paid";
            payment.Type = dto.PaymentType;
            await _paymentRepository.UpdateAsync(payment);
        }
    }
}