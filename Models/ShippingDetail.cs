using Models.Base;

namespace Models
{
    public class ShippingDetail : BaseModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        // Navigation Property
        public Order Order { get; set; }
    }
}
