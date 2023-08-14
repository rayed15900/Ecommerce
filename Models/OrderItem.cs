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
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
