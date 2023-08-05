using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.UserDTOs
{
    public class UserReadDTO : IDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
