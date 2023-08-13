using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        EcommerceContext _context;
        public DiscountRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
