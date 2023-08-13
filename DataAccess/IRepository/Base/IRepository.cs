using Models.Base;

namespace DataAccess.IRepository.Base
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> CreateAsync(T entity);
        public IQueryable<T> ReadAll();
        Task<T> ReadByIdAsync(object id);
        void Update(T entity, T oldEntity);
        void Delete(T entity);
        Task DeleteAllAsync();
    }
}
