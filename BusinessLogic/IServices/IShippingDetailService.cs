using BusinessLogic.DTOs.ShippingDetailDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IShippingDetailService : IService<ShippingDetailCreateDTO, ShippingDetailReadDTO, ShippingDetailUpdateDTO, ShippingDetail>
    {
    }
}
