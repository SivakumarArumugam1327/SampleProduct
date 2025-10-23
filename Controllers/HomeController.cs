using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myproducts.Middlewares;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [Produces("application/json", "application/xml")]
        [AuditLogs]
        [Authorize]
        [HttpGet("secure-data")]
        [ActionName("secure-data")]
        public IActionResult GetSecureData()
        {
            var user = User.Identity?.Name;
            return Ok("✅ This is a protected API endpoint accessed with a valid JWT token.");
        }

        [AllowAnonymous]
        [HttpGet("public-data")]
        public IActionResult GetPublicData()
        {
            return Ok("🌐 This is a public endpoint.");
        }

        [HttpGet("data")]
        public IActionResult GetData()
        {
            var response = new
            {
                Id = 1,
                Name = "Siva Kumar",
                Role = "Developer"
            };

            // Use content negotiation automatically
            return Ok(response);
        }
    }
}