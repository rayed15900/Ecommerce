using BusinessLogic.DTOs.PaymentDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IPaymentService : IService<PaymentCreateDTO, PaymentReadDTO, PaymentUpdateDTO, Payment>
    {
        Task Pay();
    }
}
