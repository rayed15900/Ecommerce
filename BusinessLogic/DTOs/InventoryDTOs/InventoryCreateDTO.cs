using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.InventoryDTOs
{
    public class InventoryCreateDTO : IDTO
    {
        public int Quantity { get; set; }
    }
}
