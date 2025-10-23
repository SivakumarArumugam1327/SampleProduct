using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myproducts.Middlewares;
using Myproducts.Models;
using Myproducts.Repositories;



namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repo;

        public EmployeeController(EmployeeRepository repo)
        {
            _repo = repo;
        }


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

        [HttpGet("Get")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            await _repo.AddAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            await _repo.UpdateAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}