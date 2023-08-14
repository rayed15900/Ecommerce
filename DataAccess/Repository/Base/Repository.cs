using Models.Base;
using Microsoft.EntityFrameworkCore;
using DataAccess.IRepository.Base;
using DataAccess.Context;

namespace DataAccess.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        EcommerceContext _context;

        public Repository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            return addedEntity.Entity;
        }

        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> ReadByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity, T oldEntity)
        {
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(T entity)
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
