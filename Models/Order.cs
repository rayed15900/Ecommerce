using Models.Base;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual User Order_User { get; set; }
        [JsonIgnore]
        public virtual ShippingDetail Order_ShippingDetail { get; set; }
        [JsonIgnore]
        public virtual Payment Order_Payment { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set;}

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
