using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.PaymentDTOs;

namespace BusinessLogic.IServices
{
    public interface IPaymentService : IService<PaymentCreateDTO, PaymentReadDTO, PaymentUpdateDTO, Payment>
    {
        Task Pay(int orderId, PayDTO dto);
    }
}
