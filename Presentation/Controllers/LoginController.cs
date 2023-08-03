//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private IConfiguration _config;
//        public LoginController(IConfiguration configuration)
//        {
//            _config = configuration;
//        }

//        private User AuthenticateUser(User user)
//        {
//            User _user = null;
//            if (user.Username == "admin" && user.Password == "1234")
//            {
//                _user = new User { Username = "Rayed" };
//            }
//            return _user;
//        }

//        private string GenerateToken(User user)
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
//                expires: DateTime.Now.AddMinutes(2),
//                signingCredentials: credentials
//                );
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public IActionResult Login(User user)
//        {
//            IActionResult response = Unauthorized();
//            var _user = AuthenticateUser(user);
//            if (_user != null)
//            {
//                var token = GenerateToken(_user);
//                response = Ok(new { token = token });
//            }
//            return response;
//        }
//    }
//}
