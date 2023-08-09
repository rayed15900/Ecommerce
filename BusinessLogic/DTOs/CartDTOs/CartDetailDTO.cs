using BusinessLogic.IDTOs;
using Models;

namespace BusinessLogic.DTOs.CartDTOs
{
    public class CartDetailDTO : IDTO
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        // Foreign Key
        public int UserId { get; set; }
    }
}
