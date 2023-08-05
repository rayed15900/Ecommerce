using Models.Base;

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
        public Inventory Inventory { get; set; }
        public CartItem CartItem { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
