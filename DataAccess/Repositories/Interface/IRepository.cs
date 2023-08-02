using Models.Base;

namespace DataAccess.Repositories.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
        Task CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        void Update(T entity, T oldEntity);
        void Remove(T entity);
    }
}
