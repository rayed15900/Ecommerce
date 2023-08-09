using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class ShippingDetailService : Service<ShippingDetailCreateDTO, ShippingDetailReadDTO, ShippingDetailUpdateDTO, ShippingDetail>, IShippingDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public ShippingDetailService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        
        public async Task<ShippingDetailCreateDTO> CreateShippingDetailAsync(ShippingDetailCreateDTO dto, int userId)
        {
            var shippingDtailList = await _uow.GetRepository<ShippingDetail>().GetAllAsync();

            foreach(var item in  shippingDtailList)
            {
                if(item.UserId == userId)
                {
                    return null;
                }
            }

            var createdEntity = _mapper.Map<ShippingDetail>(dto);
            createdEntity.UserId = userId;
            await _uow.GetRepository<ShippingDetail>().CreateAsync(createdEntity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<ShippingDetailCreateDTO>(createdEntity);
        }

        public async Task<ShippingDetailUpdateDTO> UpdateShippingDetailAsync(ShippingDetailUpdateDTO dto)
        {
            var oldEntity = await _uow.GetRepository<ShippingDetail>().GetByIdAsync(dto.Id);
            if (oldEntity != null)
            {
                var entity = _mapper.Map<ShippingDetail>(dto);
                entity.UserId = oldEntity.UserId;
                _uow.GetRepository<ShippingDetail>().Update(entity, oldEntity);
                await _uow.SaveChangesAsync();
            }
            var ent = _mapper.Map<ShippingDetailUpdateDTO>(oldEntity);
            return ent;
        }
    }
}
