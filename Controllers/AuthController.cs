using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Myproducts;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Myproducts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {

            if (user.Username == "admin" && user.Password == "password")
            {
                var token = GenerateToken(user);
                return Ok(new { token });
            }

            return Unauthorized("Invalid credentials");
        }

        private string GenerateToken(UserModel user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

             return new JwtSecurityTokenHandler().WriteToken(token);
           // return  JsonConvert.SerializeObject(token);
        }
    }
}