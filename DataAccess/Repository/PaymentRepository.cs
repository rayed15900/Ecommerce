using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        EcommerceContext _context;
        public PaymentRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
