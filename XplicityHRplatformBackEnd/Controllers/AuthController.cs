
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public AuthController(HRplatformDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreate)
        {
            var user = new User
            {
                UserName = userCreate.Email,
                Email = userCreate.Email
            };
            var result =  await _userManager.CreateAsync(user, userCreate.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Password has to contain alphanumeric symbols");

            }
            else
            {
                return Ok("user created");
            }
          
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, userLogin.Password);

            if (!passwordCorrect)
            {
                return BadRequest("Wrong password");
            }
            if (passwordCorrect)
            {
                var tokenString = new JwtSecurityTokenHandler().WriteToken(GenerateJWT(user));
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }



        [HttpDelete, Route("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
            return Ok("user deleted");
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

    }
}


