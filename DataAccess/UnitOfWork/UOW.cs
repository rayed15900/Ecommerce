using Models.Base;
using DataAccess.Context;
using DataAccess.IRepository.Base;
using DataAccess.Repository.Base;

namespace DataAccess.UnitOfWork
{
    public class UOW : IUOW
    {
        private readonly EcommerceContext _context;

        public UOW(EcommerceContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseModel
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
