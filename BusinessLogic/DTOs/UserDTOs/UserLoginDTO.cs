using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.UserDTOs
{
    public class UserLoginDTO : IDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
