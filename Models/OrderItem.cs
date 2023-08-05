using Models.Base;

namespace Models
{
    public class OrderItem : BaseModel
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}
