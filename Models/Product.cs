using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        public int InventoryId { get; set; }
        public int DiscountId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Inventory Inventory { get; set; }
        [JsonIgnore]
        public virtual Discount Discount { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        [JsonIgnore]
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
