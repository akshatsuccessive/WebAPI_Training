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
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await context.Departments.ToListAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] Guid id)
        {
            var department = await context.Departments.FindAsync(id);
            if(department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost] 
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDTO request)
        { 
            if(request == null)
            {
                return BadRequest();
            }
            var newDepartment = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = request.DepartmentName
            };

            await context.Departments.AddAsync(newDepartment);
            await context.SaveChangesAsync();
            //return CreatedAtRoute("GetDepartmentById", new { id = newDepartment.DepartmentId }, newDepartment);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.DepartmentId }, newDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] UpdateDepartmentDTO request)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
            if(department == null)
            {
                return NotFound();
            }
            
            department.DepartmentName = request.DepartmentName;

            await context.SaveChangesAsync();
            return Ok(department);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            context.Remove(department);
            await context.SaveChangesAsync();
            return Ok(department);
        }
    }
}
