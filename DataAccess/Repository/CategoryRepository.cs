using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        EcommerceContext _context;
        public CategoryRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
