using Models.Base;
using DataAccess.IRepository.Base;

namespace DataAccess.UnitOfWork
{
    public interface IUOW
    {
        IRepository<T> GetRepository<T>() where T : BaseModel;
        Task SaveChangesAsync();
    }
}
