using Models.Base;

namespace Models
{
    public class Order : BaseModel
    {
        public double TotalAmount { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }

        // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set;}
        public User User { get; set; }
        public Payment Payment { get; set; }
        public ShippingDetail ShippingDetail { get; set; }
    }
}
