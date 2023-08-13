using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        EcommerceContext _context;
        public InventoryRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
