using Models.Base;

namespace Models
{
    public class Order : BaseModel
    {
        public string Status { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public int ShippingDetailId { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
        public virtual ShippingDetail ShippingDetail { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set;} = new HashSet<OrderItem>();
    }
}
