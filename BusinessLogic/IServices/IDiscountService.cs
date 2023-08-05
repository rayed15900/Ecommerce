using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IDiscountService : IService<DiscountCreateDTO, DiscountReadDTO, DiscountUpdateDTO, Discount>
    {
    }
}
