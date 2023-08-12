using Models;
using MapsterMapper;
using DataAccess.UnitOfWork;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.DTOs.CategoryDTOs;

namespace BusinessLogic.Services
{
    public class InventoryService : Service<InventoryCreateDTO, InventoryReadAllDTO, InventoryUpdateDTO, Inventory>, IInventoryService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public InventoryService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<InventoryReadByIdDTO> InventoryReadByIdAsync(int id)
        {
            var inventoryData = await _uow.GetRepository<Inventory>().ReadByIdAsync(id);

            var dto = new InventoryReadByIdDTO
            {
                Id = id,
                ProductName = inventoryData.Inventory_Product.Name,
                Quantity = inventoryData.Quantity
            };

            return dto;
        }
    }
}
