using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        EcommerceContext _context;
        public OrderItemRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
