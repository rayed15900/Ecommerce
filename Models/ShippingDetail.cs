using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class ShippingDetail : BaseModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Foreign Key
        public int UserId { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual User ShippingDetail_User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        public ShippingDetail()
        {
            Orders = new List<Order>();
        }
    }
}
