using Models.Base;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using DataAccess.IRepository.Base;

namespace DataAccess.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
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
            _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
