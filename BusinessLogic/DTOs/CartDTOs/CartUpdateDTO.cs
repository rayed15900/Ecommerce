using BusinessLogic.DTOs.Interfaces;

namespace BusinessLogic.DTOs.CartDTOs
{
    public class CartUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }
    }
}
