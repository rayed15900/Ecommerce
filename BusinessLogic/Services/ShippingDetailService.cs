using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.DTOs.InventoryDTOs;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ShippingDetailService : Service<ShippingDetailCreateDTO, ShippingDetailReadAllDTO, ShippingDetailUpdateDTO, ShippingDetail>, IShippingDetailService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ShippingDetail> _shippingDetailRepository;

        public ShippingDetailService(
            IMapper mapper,
            IRepository<ShippingDetail> shippingDetailRepository) 
            : base(mapper, shippingDetailRepository)
        {
            _mapper = mapper;
            _shippingDetailRepository = shippingDetailRepository;
        }

        public async Task<ShippingDetailCreateDTO> ShippingDetailCreateAsync(ShippingDetailCreateDTO dto, int userId)
        {
            var matchShippingDetail = await _shippingDetailRepository.ReadAll().AnyAsync(item => item.UserId == userId);

            if (matchShippingDetail)
            {
                return null;
            }

            var shippingDetail = _mapper.Map<ShippingDetail>(dto);
            shippingDetail.UserId = userId;

            var createdShippingDetail = await _shippingDetailRepository.CreateAsync(shippingDetail);
            var shippingDetailDTO = _mapper.Map<ShippingDetailCreateDTO>(createdShippingDetail);

            return shippingDetailDTO;
        }

        public async Task<ShippingDetailReadByIdDTO> ShippingDetailReadByIdAsync(int id)
        {
            var shippingDetail = await _shippingDetailRepository.ReadByIdAsync(id);

            var dto = new ShippingDetailReadByIdDTO
            {
                Id = id,
                Username = shippingDetail.User.Username,
                Country = shippingDetail.Country,
                City = shippingDetail.City,
                Address = shippingDetail.Address,
                Phone = shippingDetail.Phone
            };

            return dto;
        }

        public async Task<ShippingDetailUpdateDTO> ShippingDetailUpdateAsync(ShippingDetailUpdateDTO dto)
        {
            var shippingDetail = await _shippingDetailRepository.ReadByIdAsync(dto.Id);
            if (shippingDetail != null)
            {
                var newShippingDetail = _mapper.Map<ShippingDetail>(dto);
                newShippingDetail.UserId = shippingDetail.UserId;

                await _shippingDetailRepository.UpdateAsync(newShippingDetail);
            }
            var shippingDetailDTO = _mapper.Map<ShippingDetailUpdateDTO>(shippingDetail);
            return shippingDetailDTO;
        }
    }
}
