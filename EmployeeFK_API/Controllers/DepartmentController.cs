using EmployeeFK_API.Data;
using EmployeeFK_API.Data.Base;
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
        private readonly IDepartmentRepository dept;
        public DepartmentController(IDepartmentRepository dept)
        {
            this.dept = dept;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await dept.GetDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            var department = await dept.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var newDepartment = await dept.AddDepartment(request);

                //return CreatedAtRoute("GetDepartmentById", new { id = newDepartment.DepartmentId }, newDepartment);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.DepartmentId }, newDepartment);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, [FromBody] UpdateDepartmentDTO request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            try
            {
                var department = await dept.UpdateDepartment(id, request);
                if (department == null)
                {
                    return NotFound();
                }

                return Ok(department);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var department = await dept.DeleteDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}
