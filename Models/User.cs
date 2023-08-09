using Models.Base;

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
        public ShippingDetail ShippingDetail { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
