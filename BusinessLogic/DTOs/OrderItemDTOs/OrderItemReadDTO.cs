using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.OrderItemDTOs
{
    public class OrderItemReadDTO : IDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
