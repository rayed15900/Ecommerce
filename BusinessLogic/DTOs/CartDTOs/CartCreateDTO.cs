using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.CartDTOs
{
    public class CartCreateDTO : IDTO
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }
    }
}
