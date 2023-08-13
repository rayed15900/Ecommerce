using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        EcommerceContext _context;
        public CartItemRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
