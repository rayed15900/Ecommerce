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
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ShippingDetail ShippingDetail { get; set; }
        [JsonIgnore]
        public virtual Payment Payment { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderItem> OrderItems { get; set;} = new HashSet<OrderItem>();
    }
}
