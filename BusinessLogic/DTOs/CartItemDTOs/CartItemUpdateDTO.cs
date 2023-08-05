using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CartItemDTOs
{
    public class CartItemUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        // Foreign Key
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}
