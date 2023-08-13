using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        EcommerceContext _context;
        public OrderRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
