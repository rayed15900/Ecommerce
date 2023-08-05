using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.OrderItemDTOs
{
    public class OrderItemCreateDTO : IDTO
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
