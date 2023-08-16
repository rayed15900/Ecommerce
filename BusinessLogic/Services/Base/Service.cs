using Models.Base;
using MapsterMapper;
using BusinessLogic.IDTOs;
using BusinessLogic.IServices.Base;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services.Base
{
    public class Service<CreateDTO, ReadAllDTO, UpdateDTO, T> : IService<CreateDTO, ReadAllDTO, UpdateDTO, T>
        where CreateDTO : class, IDTO, new()
        where ReadAllDTO : class, IDTO, new()
        where UpdateDTO : class, IUpdateDTO, new()
        where T : BaseModel
    {
        private readonly IMapper _mapper;
        private readonly IRepository<T> _repository;

        public Service(IMapper mapper, IRepository<T> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreateDTO> CreateAsync(CreateDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            var createdEntity = await _repository.CreateAsync(entity);
            return _mapper.Map<CreateDTO>(createdEntity);
        }

        public async Task<List<ReadAllDTO>> ReadAllAsync()
        {
            var data = await _repository.ReadAll().ToListAsync();
            var dto = _mapper.Map<List<ReadAllDTO>>(data);
            return dto;
        }

        public async Task<IDTO> ReadByIdAsync<IDTO>(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            var dto = _mapper.Map<IDTO>(data);
            return dto;
        }

        public async Task<T> UpdateAsync(UpdateDTO dto)
        {
            var entity = _mapper.Map<T>(dto);
            bool result = await _repository.UpdateAsync(entity);
            if(result)
            {
                return entity;
            }
            return null;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var data = await _repository.ReadByIdAsync(id);
            if (data != null)
            {
                await _repository.DeleteAsync(data);
            }
            return data;
        }
    }
}
