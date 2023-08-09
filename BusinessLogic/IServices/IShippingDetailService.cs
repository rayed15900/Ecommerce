using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IShippingDetailService : IService<ShippingDetailCreateDTO, ShippingDetailReadDTO, ShippingDetailUpdateDTO, ShippingDetail>
    {
        Task<ShippingDetailCreateDTO> CreateShippingDetailAsync(ShippingDetailCreateDTO dto, int userId);
        Task<ShippingDetailUpdateDTO> UpdateShippingDetailAsync(ShippingDetailUpdateDTO dto);
    }
}
