using Models.Base;
using BusinessLogic.IDTOs;

namespace BusinessLogic.IServices.Base
{
    public interface IService<CreateDTO, ReadAllDTO, UpdateDTO, T>
        where CreateDTO : class, IDTO, new()
        where ReadAllDTO : class, IDTO, new()
        where UpdateDTO : class, IUpdateDTO, new()
        where T : BaseModel
    {
        Task<CreateDTO> CreateAsync(CreateDTO dto);
        Task<List<ReadAllDTO>> ReadAllAsync();
        Task<IDTO> ReadByIdAsync<IDTO>(int id);
        Task<T> UpdateAsync(UpdateDTO dto);
        Task<T> DeleteAsync(int id);
        Task DeleteAllAsync();
    }
}
