using EmployeesAPI.Data;
using EmployeesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeesDBContext context;
        public EmployeeController(EmployeesDBContext context)
        {
            this.context = context;
        }

        //[HttpGet("All")]  // https://localhost:7150/api/Employees/All
        [HttpGet]           // https://localhost:7150/api/Employees/
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllEmployees()
        {
            return Ok(context.Employees.ToList());
        }

        // Get a single employee
        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var employee = await context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployeeByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Name == name);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            if(employee == null)
            {
                return BadRequest(employee);
            }
            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            //return Ok(employee);
            return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);   // for line 26 (same name)
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {

            var currentEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            int currentSalary = currentEmployee.Salary;

            if (currentEmployee != null)
            {
                if (updatedEmployee.Name.Length != 0)
                {
                    currentEmployee.Name = updatedEmployee.Name;
                }
                if (updatedEmployee.Email.Length != 0)
                {
                    currentEmployee.Email = updatedEmployee.Email;
                }
                if (updatedEmployee.Department.Length != 0)
                {
                    currentEmployee.Department = updatedEmployee.Department;
                }

                if (updatedEmployee.Salary == 0)
                {
                    currentEmployee.Salary = currentSalary;
                }
                else
                {
                    currentEmployee.Salary = updatedEmployee.Salary;
                }

                await context.SaveChangesAsync();

                return Ok(currentEmployee);     // returning the response back
            }
            else
            {
                return NotFound();
            }

            /*
            var currentEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);


            if (currentEmployee != null)
            {
                currentEmployee.Name = updatedEmployee.Name;
                currentEmployee.Email = updatedEmployee.Email;
                currentEmployee.Salary = updatedEmployee.Salary;
                currentEmployee.Department = updatedEmployee.Department;

                await context.SaveChangesAsync();

                return Ok(currentEmployee);     // returning the response back
            }
            else
            {
                return NotFound();
            }
            */
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
