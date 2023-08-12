using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.ShippingDetailDTOs;

namespace BusinessLogic.IServices
{
    public interface IShippingDetailService : IService<ShippingDetailCreateDTO, ShippingDetailReadAllDTO, ShippingDetailUpdateDTO, ShippingDetail>
    {
        Task<ShippingDetailCreateDTO> ShippingDetailCreateAsync(ShippingDetailCreateDTO dto, int userId);
        Task<ShippingDetailReadByIdDTO> ShippingDetailReadByIdAsync(int id);
        Task<ShippingDetailUpdateDTO> ShippingDetailUpdateAsync(ShippingDetailUpdateDTO dto);
    }
}
