using DataAccess.UnitOfWork.Interface;
using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.Services.Base;
using BusinessLogic.IServices;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class ProductService : Service<ProductCreateDTO, ProductReadDTO, ProductUpdateDTO, Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public ProductService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ProductCreateDTO> ProductCreateAsync(ProductCreateDTO dto)
        {
            var createdEntity = _mapper.Map<Product>(dto);

            Inventory newInventory = new Inventory();
            newInventory.Quantity = dto.Quantity;
            var inv = await _uow.GetRepository<Inventory>().CreateAsync(newInventory);
            await _uow.SaveChangesAsync();

            createdEntity.InventoryId = inv.Id;

            await _uow.GetRepository<Product>().CreateAsync(createdEntity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<ProductCreateDTO>(dto);
        }

        public async Task<ProductUpdateDTO> ProductUpdateAsync(ProductUpdateDTO dto)
        {
            var oldEntity = await _uow.GetRepository<Product>().GetByIdAsync(dto.Id);
            if (oldEntity != null)
            {
                var entity = _mapper.Map<Product>(dto);
                entity.InventoryId = oldEntity.InventoryId;
                _uow.GetRepository<Product>().Update(entity, oldEntity);
                await _uow.SaveChangesAsync();

                var oldInvEntity = await _uow.GetRepository<Inventory>().GetByIdAsync(oldEntity.InventoryId);
                
                if(oldInvEntity != null)
                {
                    var newInvEntity = new Inventory
                    {
                        Id = oldEntity.InventoryId,
                        Quantity = dto.Quantity
                    };

                    _uow.GetRepository<Inventory>().Update(newInvEntity, oldInvEntity);
                    await _uow.SaveChangesAsync();
                }
            }
            var ent = _mapper.Map<ProductUpdateDTO>(oldEntity);
            ent.Quantity = dto.Quantity;
            return ent;
        }

        public async Task<ProductDetailDTO> ProductDetailAsync(int id)
        {
            var productData = await _uow.GetRepository<Product>().GetByIdAsync(id);
            var inventoryData = await _uow.GetRepository<Inventory>().GetByIdAsync(productData.InventoryId);
            var categoryData = await _uow.GetRepository<Category>().GetByIdAsync(productData.CategoryId);
            var discountData = await _uow.GetRepository<Discount>().GetByIdAsync(productData.DiscountId);

            double disPrice = 0;

            if (discountData.Active)
            {
                disPrice = productData.Price * (discountData.Percent / 100);
            }

            var dto = new ProductDetailDTO
            {
                Id = productData.Id,
                Name = productData.Name,
                Description = productData.Description,
                OriginalPrice = productData.Price,
                PriceReduced = disPrice,
                DiscountedPrice = discountData.Active ? (productData.Price - disPrice) : 0,
                DiscountPercent = discountData.Active ? discountData.Percent : 0,
                Category = categoryData.Name,
                Quantity = inventoryData.Quantity
            };

            return dto;
        }
    }
}
