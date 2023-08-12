using Models.Base;
using System.Text.Json.Serialization;

namespace Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // Navigation Property
        [JsonIgnore]
        public virtual ShippingDetail User_ShippingDetail { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
