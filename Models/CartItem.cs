using Models.Base;

namespace Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}
