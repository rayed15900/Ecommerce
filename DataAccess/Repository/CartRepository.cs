using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        EcommerceContext _context;
        public CartRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
