using DataAccess.Context;
using DataAccess.IRepository;
using DataAccess.Repository.Base;
using Models;

namespace DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        EcommerceContext _context;
        public UserRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
