using Models.Base;

namespace DataAccess.Repositories.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<int?> GetFirstIdAsync();
        void Update(T entity, T oldEntity);
        void Remove(T entity);
        Task DeleteAllAsync();
    }
}
