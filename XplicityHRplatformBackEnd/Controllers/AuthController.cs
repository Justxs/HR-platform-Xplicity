
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XplicityHRplatformBackEnd.DB;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HRplatformDbContext _dbContext;

        public AuthController(HRplatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User userLogin)
        {
            var user = AuthenticateUser(userLogin);

            if (user == null)
            {
                return BadRequest("Invalid user request"); 
            }
            if (user != null)
            {
                var tokenString = new JwtSecurityTokenHandler().WriteToken(GenerateJWT(user));
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] User userRegister)
        {
            var userExist = _dbContext.users.Any(x => x.Email == userRegister.Email);

            if (!userExist)
            {
                var user = new User
                {
                    UserName = userRegister.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userRegister.Password),
                    Email = userRegister.Email
                };
                _dbContext.users.Add(user);
                _dbContext.SaveChanges();

                var tokenString = new JwtSecurityTokenHandler().WriteToken(GenerateJWT(user));
                return Ok(new { Token = tokenString });
            }
            else
            {
                return BadRequest("User already exists");
            }
        }

        private JwtSecurityToken GenerateJWT(User login)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyUltraSecretKeyForHrApp"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7241",
                audience: "https://localhost:7241",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials
                );

            return tokenOptions;
        }

        private User AuthenticateUser(User login)
        {
            var user = _dbContext.users.Where(x => x.Email == login.Email).FirstOrDefault();

            if (user != null && BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                user = new User { UserName = user.Email, Email = user.Email };
                return user;
            }
            else
            {
                return null;
            }

        }
    }
}
