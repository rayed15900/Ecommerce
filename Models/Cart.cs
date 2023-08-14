using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Cart : BaseModel
    {
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public bool IsGuest { get; set; }

        // Navigation Property
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
