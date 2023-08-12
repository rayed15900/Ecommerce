using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class OrderItem : BaseModel
    {
        public int Quantity { get; set; }

        // Foreign Key
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual Order OrderItem_Order { get; set; }
        [JsonIgnore]
        public virtual Product OrderItem_Product { get; set; }
    }
}
