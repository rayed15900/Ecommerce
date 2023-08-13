using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class ShippingDetailRepository : Repository<ShippingDetail>, IShippingDetailRepository
    {
        EcommerceContext _context;
        public ShippingDetailRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
