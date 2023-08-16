using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.DiscountDTOs;
using DataAccess.IRepository.Base;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.DTOs.CategoryDTOs;

namespace BusinessLogic.Services
{
    public class DiscountService : Service<DiscountCreateDTO, DiscountReadAllDTO, DiscountUpdateDTO, Discount>, IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Discount> _discountRepository;

        public DiscountService(
            IMapper mapper,
            IRepository<Discount> discountRepository) 
            : base(mapper, discountRepository)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
        }

        public async Task<DiscountReadByIdDTO> DiscountReadByIdAsync(int id)
        {
            var discount = await _discountRepository.ReadByIdAsync(id);

            if (discount == null)
            {
                return null;
            }

            var productList = discount.Products.Select(item => new DiscountProductReadDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Inventory.Quantity
            }).ToList();

            var dto = new DiscountReadByIdDTO
            {
                Id = id,
                Name = discount.Name,
                Percent = discount.Percent,
                Active = discount.Active,
                Products = productList
            };

            return dto;
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            bool isNameUnique = !(await _discountRepository.ReadAll().AnyAsync(item => item.Name == name));
            return isNameUnique;
        }
    }
}
