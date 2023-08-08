using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CartItemDTOs
{
    public class CartItemCreateDTO : IDTO
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
    }
}
