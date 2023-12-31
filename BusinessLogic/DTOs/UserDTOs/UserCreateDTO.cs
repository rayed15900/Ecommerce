﻿using BusinessLogic.IDTOs;

namespace BusinessLogic.DTOs.UserDTOs
{
    public class UserCreateDTO : IDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
