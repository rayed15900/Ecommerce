using BusinessLogic.DTOs.Interfaces;
using Models.Base;

namespace BusinessLogic.IServices.Base
{
    public interface IService<CreateDTO, ReadDTO, UpdateDTO, T>
        where CreateDTO : class, IDTO, new()
        where ReadDTO : class, IDTO, new()
        where UpdateDTO : class, IUpdateDTO, new()
        where T : BaseModel
    {
        Task<CreateDTO> CreateAsync(CreateDTO dto);
        Task<List<ReadDTO>> GetAllAsync();
        Task<IDTO> GetByIdAsync<IDTO>(int id);
        Task<T> UpdateAsync(UpdateDTO dto);
        Task<T> RemoveAsync(int id);
    }
}
