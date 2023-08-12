using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CartItemDTOs
{
    public class CartItemReadByIdDTO : IDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        // Foreign Key
        public int CartId { get; set; }
    }
}
