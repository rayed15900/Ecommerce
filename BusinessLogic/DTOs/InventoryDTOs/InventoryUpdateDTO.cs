using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.InventoryDTOs
{
    public class InventoryUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
