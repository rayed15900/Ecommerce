using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.InventoryDTOs;

namespace BusinessLogic.IServices
{
    public interface IInventoryService : IService<InventoryCreateDTO, InventoryReadAllDTO, InventoryUpdateDTO, Inventory>
    {
        Task<InventoryReadByIdDTO> InventoryReadByIdAsync(int id);
    }
}
