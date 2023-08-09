using Models.Base;

namespace Models
{
    public class Order : BaseModel
    {
        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }

        // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set;}
        public Payment Payment { get; set; }
        public ShippingDetail ShippingDetail { get; set; }
    }
}
