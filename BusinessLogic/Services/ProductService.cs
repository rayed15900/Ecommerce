//using Models;
//using MapsterMapper;
//using DataAccess.UnitOfWork;
//using BusinessLogic.IServices;
//using BusinessLogic.Services.Base;
//using BusinessLogic.DTOs.ProductDTOs;

//namespace BusinessLogic.Services
//{
//    public class ProductService : Service<ProductCreateDTO, ProductReadAllDTO, ProductUpdateDTO, Product>, IProductService
//    {
//        private readonly IMapper _mapper;
//        private readonly IUOW _uow;

//        public ProductService(IMapper mapper, IUOW uow) : base(mapper, uow)
//        {
//            _mapper = mapper;
//            _uow = uow;
//        }

//        public async Task<ProductCreateDTO> ProductCreateAsync(ProductCreateDTO dto)
//        {
//            var entity = _mapper.Map<Product>(dto);

//            var newInventory = new Inventory
//            {
//                Quantity = dto.Quantity
//            };

//            var inventoryEntity = await _uow.GetRepository<Inventory>().CreateAsync(newInventory);
//            await _uow.SaveChangesAsync();

//            entity.InventoryId = inventoryEntity.Id;

//            await _uow.GetRepository<Product>().CreateAsync(entity);
//            await _uow.SaveChangesAsync();

//            return _mapper.Map<ProductCreateDTO>(entity);
//        }

//        public async Task<ProductReadByIdDTO> ProductReadByIdAsync(int id)
//        {
//            var productData = await _uow.GetRepository<Product>().ReadByIdAsync(id);

//            double discountedPrice = 0;

//            if (productData.Product_Discount.Active)
//            {
//                discountedPrice = productData.Price * (productData.Product_Discount.Percent / 100);
//            }

//            var dto = new ProductReadByIdDTO
//            {
//                Id = productData.Id,
//                Name = productData.Name,
//                Description = productData.Description,
//                OriginalPrice = productData.Price,
//                DiscountName = productData.Product_Discount.Name,
//                PriceReduced = discountedPrice,
//                DiscountPercent = productData.Product_Discount.Percent,
//                FinalPrice = productData.Product_Discount.Active ? (productData.Price - discountedPrice) : productData.Price,
//                Category = productData.Product_Category.Name,
//                Quantity = productData.Product_Inventory.Quantity
//            };

//            return dto;
//        }

//        public async Task<ProductUpdateDTO> ProductUpdateAsync(ProductUpdateDTO dto)
//        {
//            var oldProductEntity = await _uow.GetRepository<Product>().ReadByIdAsync(dto.Id);

//            if (oldProductEntity == null)
//            {
//                return null;
//            }

//            var newProductEntity = _mapper.Map<Product>(dto);
//            newProductEntity.InventoryId = oldProductEntity.InventoryId;

//            _uow.GetRepository<Product>().Update(newProductEntity, oldProductEntity);

//            var oldInventoryEntity = await _uow.GetRepository<Inventory>().ReadByIdAsync(oldProductEntity.InventoryId);

//            if (oldInventoryEntity == null)
//            {
//                return null;
//            }

//            var newInventoryEntity = new Inventory
//            {
//                Id = oldProductEntity.InventoryId,
//                Quantity = dto.Quantity
//            };

//            _uow.GetRepository<Inventory>().Update(newInventoryEntity, oldInventoryEntity);

//            await _uow.SaveChangesAsync();

//            return _mapper.Map<ProductUpdateDTO>(newProductEntity);
//        }

//        public async Task<Product> ProductDeleteAsync(int id)
//        {
//            var productData = await _uow.GetRepository<Product>().ReadByIdAsync(id);
//            var inventoryData = await _uow.GetRepository<Inventory>().ReadByIdAsync(productData.InventoryId);

//            if(inventoryData != null && productData != null)
//            {
//                _uow.GetRepository<Inventory>().Delete(inventoryData);
//                _uow.GetRepository<Product>().Delete(productData);
//                await _uow.SaveChangesAsync();
//            }
//            return productData;
//        }
//    }
//}
