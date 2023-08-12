using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.InventoryDTOs
{
    public class InventoryReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
