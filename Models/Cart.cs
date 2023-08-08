using Models.Base;

namespace Models
{
    public class Cart : BaseModel
    {
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation Property
        public ICollection<CartItem> CartItems { get; set; }
    }
}
