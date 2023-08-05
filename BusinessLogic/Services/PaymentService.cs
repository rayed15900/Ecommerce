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
    }
}
