//using Models;
//using MapsterMapper;
//using DataAccess.UnitOfWork;
//using BusinessLogic.IServices;
//using BusinessLogic.Services.Base;
//using BusinessLogic.DTOs.ShippingDetailDTOs;
//using BusinessLogic.DTOs.InventoryDTOs;

//namespace BusinessLogic.Services
//{
//    public class ShippingDetailService : Service<ShippingDetailCreateDTO, ShippingDetailReadAllDTO, ShippingDetailUpdateDTO, ShippingDetail>, IShippingDetailService
//    {
//        private readonly IMapper _mapper;
//        private readonly IUOW _uow;

//        public ShippingDetailService(IMapper mapper, IUOW uow) : base(mapper, uow)
//        {
//            _mapper = mapper;
//            _uow = uow;
//        }

        
//        public async Task<ShippingDetailCreateDTO> ShippingDetailCreateAsync(ShippingDetailCreateDTO dto, int userId)
//        {
//            var shippingDtailList = _uow.GetRepository<ShippingDetail>().ReadAll().ToList();

//            foreach(var item in shippingDtailList)
//            {
//                if(item.UserId == userId)
//                {
//                    return null;
//                }
//            }

//            var createdEntity = _mapper.Map<ShippingDetail>(dto);
//            createdEntity.UserId = userId;
//            await _uow.GetRepository<ShippingDetail>().CreateAsync(createdEntity);
//            await _uow.SaveChangesAsync();
//            return _mapper.Map<ShippingDetailCreateDTO>(createdEntity);
//        }

//        public async Task<ShippingDetailReadByIdDTO> ShippingDetailReadByIdAsync(int id)
//        {
//            var shippingData = await _uow.GetRepository<ShippingDetail>().ReadByIdAsync(id);

//            var dto = new ShippingDetailReadByIdDTO
//            {
//                Id = id,
//                Username = shippingData.ShippingDetail_User.Username,
//                Country = shippingData.Country,
//                City = shippingData.City,
//                Address = shippingData.Address,
//                Phone = shippingData.Phone
//            };

//            return dto;
//        }

//        public async Task<ShippingDetailUpdateDTO> ShippingDetailUpdateAsync(ShippingDetailUpdateDTO dto)
//        {
//            var oldEntity = await _uow.GetRepository<ShippingDetail>().ReadByIdAsync(dto.Id);
//            if (oldEntity != null)
//            {
//                var entity = _mapper.Map<ShippingDetail>(dto);
//                entity.UserId = oldEntity.UserId;
//                _uow.GetRepository<ShippingDetail>().Update(entity, oldEntity);
//                await _uow.SaveChangesAsync();
//            }
//            var ent = _mapper.Map<ShippingDetailUpdateDTO>(oldEntity);
//            return ent;
//        }
//    }
//}
