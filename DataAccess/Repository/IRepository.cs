using Models.Base;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> ReadAllAsync();
        Task<T> ReadByIdAsync(object id);
        void Update(T entity, T oldEntity);
        void Delete(T entity);
        Task DeleteAllAsync();
    }
}
