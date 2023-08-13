using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        EcommerceContext _context;
        public ProductRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
