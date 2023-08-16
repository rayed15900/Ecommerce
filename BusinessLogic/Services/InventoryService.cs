using Models;
using MapsterMapper;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.InventoryDTOs;
using DataAccess.IRepository.Base;

namespace BusinessLogic.Services
{
    public class InventoryService : Service<InventoryCreateDTO, InventoryReadAllDTO, InventoryUpdateDTO, Inventory>, IInventoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Inventory> _inventoryRepository;

        public InventoryService(
            IMapper mapper, 
            IRepository<Inventory> inventoryRepository) 
            : base(mapper, inventoryRepository)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryReadByIdDTO> InventoryReadByIdAsync(int id)
        {
            var inventory = await _inventoryRepository.ReadByIdAsync(id);

            if (inventory == null || inventory.Product == null)
            {
                return null;
            }

            var dto = new InventoryReadByIdDTO
            {
                Id = id,
                ProductName = inventory.Product.Name,
                Quantity = inventory.Quantity
            };

            return dto;
        }
    }
}
