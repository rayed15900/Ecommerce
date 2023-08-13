using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.DiscountDTOs;
using BusinessLogic.DTOs.CategoryDTOs;

namespace BusinessLogic.Services
{
    public class DiscountService : Service<DiscountCreateDTO, DiscountReadAllDTO, DiscountUpdateDTO, Discount>, IDiscountService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public DiscountService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<DiscountReadByIdDTO> DiscountReadByIdAsync(int id)
        {
            var discountData = await _uow.GetRepository<Discount>().ReadByIdAsync(id);

            var productData = _uow.GetRepository<Product>().ReadAll().ToList();

            var productList = new List<DiscountProductReadDTO>();

            foreach (var item in productData)
            {
                if (item.DiscountId == id)
                {
                    var productDTO = new DiscountProductReadDTO
                    {
                        ProductId = item.Id,
                        ProductName = item.Name
                    };
                    productList.Add(productDTO);
                }
            }

            var dto = new DiscountReadByIdDTO
            {
                Id = id,
                Name = discountData.Name,
                Percent = discountData.Percent,
                Active = discountData.Active,
                Products = productList
            };

            return dto;
        }

        public async Task<bool> IsNameUniqueAsync(string name)
        {
            var list = _uow.GetRepository<Discount>().ReadAll().ToList();

            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
