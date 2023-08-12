using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Cart : BaseModel
    {
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}
