using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.DiscountDTOs;

namespace BusinessLogic.IServices
{
    public interface IDiscountService : IService<DiscountCreateDTO, DiscountReadAllDTO, DiscountUpdateDTO, Discount>
    {
        Task<DiscountReadByIdDTO> DiscountReadByIdAsync(int id);
        Task<bool> IsNameUniqueAsync(string name);
    }
}
