using Models.Base;
using MapsterMapper;
using BusinessLogic.IDTOs;
using BusinessLogic.IServices.Base;
using DataAccess.IRepository.Base;
using DataAccess.UnitOfWork;

namespace BusinessLogic.Services.Base
{
    public class Service<CreateDTO, ReadAllDTO, UpdateDTO, T> : IService<CreateDTO, ReadAllDTO, UpdateDTO, T>
        where CreateDTO : class, IDTO, new()
        where ReadAllDTO : class, IDTO, new()
        where UpdateDTO : class, IUpdateDTO, new()
        where T : BaseModel
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public Service(IMapper mapper, IUOW uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<CreateDTO> CreateAsync(CreateDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _uow.GetRepository<T>().CreateAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<CreateDTO>(entity);
        }

        public async Task<List<ReadAllDTO>> ReadAllAsync()
        {
            var data = _uow.GetRepository<T>().ReadAll().ToList();  
            var dto = _mapper.Map<List<ReadAllDTO>>(data);
            return dto;
        }

        public async Task<IDTO> ReadByIdAsync<IDTO>(int id)
        {
            var data = await _uow.GetRepository<T>().ReadByIdAsync(id);
            var dto = _mapper.Map<IDTO>(data);
            return dto;
        }

        public async Task<T> UpdateAsync(UpdateDTO dto)
        {
            var oldEntity = await _uow.GetRepository<T>().ReadByIdAsync(dto.Id);
            if (oldEntity != null)
            {
                var entity = _mapper.Map<T>(dto);
                _uow.GetRepository<T>().Update(entity, oldEntity);
                await _uow.SaveChangesAsync();
            }
            return oldEntity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var data = await _uow.GetRepository<T>().ReadByIdAsync(id);
            if (data != null)
            {
                _uow.GetRepository<T>().Delete(data);
                await _uow.SaveChangesAsync();
            }
            return data;
        }

        public async Task DeleteAllAsync()
        {
            await _uow.GetRepository<T>().DeleteAllAsync();
            await _uow.SaveChangesAsync();
        }
    }
}
