using Models.Base;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;
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
            var createdEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return createdEntity.Entity;
        }

        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> ReadByIdAsync(object id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            return data;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var oldEntity = await _context.Set<T>().FindAsync(entity.Id);
            if (oldEntity != null)
            {
                _context.Entry(oldEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
