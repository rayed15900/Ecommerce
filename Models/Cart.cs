using Models.Base;

namespace Models
{
    public class Cart : BaseModel
    {
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
