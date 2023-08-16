using Models.Base;

namespace DataAccess.IRepository.Base
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> CreateAsync(T entity);
        public IQueryable<T> ReadAll();
        Task<T> ReadByIdAsync(object id);
        Task<bool> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
