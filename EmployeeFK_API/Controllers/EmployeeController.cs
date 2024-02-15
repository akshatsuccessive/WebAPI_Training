using EmployeeFK_API.Data;
using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFK_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var newEmployee = new Employee()
                {
                    EmployeeId = Guid.NewGuid(),
                    Name = request.Name,
                    Address = request.Address,
                    Age = request.Age,
                    Salary = request.Salary,
                    DepartmentId = request.DepartmentId,
                };

                await context.Employees.AddAsync(newEmployee);
                await context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployeeById),new { id = newEmployee.EmployeeId }, newEmployee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
