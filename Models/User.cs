using Models.Base;

namespace Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Navigation Property
        public Cart Cart { get; set; }
        public Order Order { get; set; }
    }
}
