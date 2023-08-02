using DataAccess.Repositories.Interface;
using Models.Base;

namespace DataAccess.UnitOfWork.Interface
{
    public interface IUOW
    {
        IRepository<T> GetRepository<T>() where T : BaseModel;
        Task SaveChangesAsync();
    }
}
