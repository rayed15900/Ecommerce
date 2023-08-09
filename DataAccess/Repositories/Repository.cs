using DataAccess.Context;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Base;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly EcommerceContext _context;

        public Repository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            return addedEntity.Entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int?> GetFirstIdAsync()
        {
            var firstRow = await _context.Set<T>().FirstOrDefaultAsync();
            if (firstRow != null)
            {
                return firstRow.Id;
            }
            return null;
        }

        public void Update(T entity, T oldEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task DeleteAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            _context.Set<T>().RemoveRange(entities);
        }
    }
}
