using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IInventoryService : IService<InventoryCreateDTO, InventoryReadDTO, InventoryUpdateDTO, Inventory>
    {
    }
}
