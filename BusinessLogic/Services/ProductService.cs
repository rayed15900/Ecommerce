using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.ProductDTOs;
using DataAccess.IRepository.Base;

namespace BusinessLogic.Services
{
    public class ProductService : Service<ProductCreateDTO, ProductReadAllDTO, ProductUpdateDTO, Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Inventory> _inventoryRepository;

        public ProductService(
            IMapper mapper, 
            IRepository<Product> productRepository,
            IRepository<Inventory> inventoryRepository) 
            : base(mapper, productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ProductCreateDTO> ProductCreateAsync(ProductCreateDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            var inventory = await CreateInventoryAsync(dto.Quantity);
            var createdProduct = await CreateProductAsync(dto, inventory.Id);

            var productDTO = _mapper.Map<ProductCreateDTO>(createdProduct);

            return productDTO;
        }

        private async Task<Inventory> CreateInventoryAsync(int quantity)
        {
            var inventory = new Inventory
            {
                Quantity = quantity
            };

            return await _inventoryRepository.CreateAsync(inventory);
        }

        private async Task<Product> CreateProductAsync(ProductCreateDTO dto, int inventoryId)
        {
            var product = _mapper.Map<Product>(dto);
            product.InventoryId = inventoryId;

            return await _productRepository.CreateAsync(product);
        }

        public async Task<ProductReadByIdDTO> ProductReadByIdAsync(int id)
        {
            var product = await _productRepository.ReadByIdAsync(id);
            double priceReduced = CalculatePriceReduced(product);

            var dto = new ProductReadByIdDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                OriginalPrice = product.Price,
                DiscountName = product.Discount.Name,
                PriceReduced = priceReduced,
                DiscountPercent = product.Discount.Percent,
                FinalPrice = product.Price - priceReduced,
                Category = product.Category.Name,
                Quantity = product.Inventory.Quantity
            };
            return dto;
        }

        private double CalculatePriceReduced(Product product)
        {
            if (product.Discount.Active)
            {
                return product.Price * (product.Discount.Percent / 100);
            }
            return 0;
        }

        public async Task<ProductUpdateDTO> ProductUpdateAsync(ProductUpdateDTO dto)
        {
            var product = await _productRepository.ReadByIdAsync(dto.Id);

            if (product == null)
            {
                return null;
            }

            // Update Product
            var productEntity = _mapper.Map<Product>(dto);
            productEntity.InventoryId = product.InventoryId;
            await _productRepository.UpdateAsync(productEntity);

            var inventory = product.Inventory;

            if (inventory == null)
            {
                return null;
            }

            // Update Inventory
            inventory.Quantity = dto.Quantity;
            await _inventoryRepository.UpdateAsync(inventory);

            return dto;
        }

        public async Task<Product> ProductDeleteAsync(int id)
        {
            var product = await _productRepository.ReadByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            var inventory = product.Inventory;

            if (inventory == null)
            {
                return null;
            }

            await _productRepository.DeleteAsync(product);
            await _inventoryRepository.DeleteAsync(inventory);

            return product;
        }
    }
}
