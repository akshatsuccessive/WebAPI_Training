using Employees_API2.Data;
using Employees_API2.Models.DomainModels;
using Employees_API2.Models.DTO;
using Employees_API2.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees_API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDBContext context;
        public EmployeesController(EmployeeDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Employee>> GetAllEmployees()
        {
            var employees = await context.Employees.ToListAsync();

            var employeesDTO = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                employeesDTO.Add(new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department
                });
            }

            return Ok(employeesDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromRoute] int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                Department = employee.Department
            };

            return Ok(employeeDTO);
        }

        [HttpGet("{name}")]
        //[HttpGet("{name:regex(^[[\\w\\s.-]]+$)}")]     // for having space
        public async Task<ActionResult<Employee>> GetEmployeeByName([FromRoute] string name)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Name == name);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDTO = new EmployeeDTO()
            {
                Id = employee.Id,
                Name = Uri.UnescapeDataString(employee.Name),
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                Salary = employee.Salary,
                Department = employee.Department
            };

            return Ok(employeeDTO);
        }

        [HttpGet("department/{department:regex(^[[\\w\\s.-]]+$)}")]
        public async Task<ActionResult<Employee>> GetEmployeesByDepartment([FromRoute] string department)
        {
            var employees = await context.Employees.Where(e => e.Department == department).ToListAsync();
            if (employees.Count == 0)
            {
                return NotFound();
            }

            var employeesDTO = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                employeesDTO.Add(new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department
                });
            }
            return Ok(employeesDTO);
        }

        [HttpGet("GreaterThanAge/{age:int}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByAge([FromRoute] int age)
        {
            DateTime currentDate = DateTime.Now;
            DateTime birthDateThreshold = currentDate.AddYears(-age);

            var employees = await context.Employees.Where(e => e.DateOfBirth <= birthDateThreshold).ToListAsync();

            if (employees.Count == 0)
            {
                return NotFound();
            }

            var employeesDTO = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                employeesDTO.Add(new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department
                });
            }
            return Ok(employeesDTO);
        }

        [HttpGet("AgeRange")]
        public async Task<IActionResult> GetEmployeesByAgeRange([FromQuery] int minAge, [FromQuery] int maxAge)
        {
            try
            {
                if (minAge < 18 || maxAge > 60 || minAge > maxAge)
                {
                    return BadRequest("Invalid Age Range");
                }

                DateTime currentDate = DateTime.Now;
                DateTime minAgeThreshold = currentDate.AddYears(-maxAge);
                DateTime maxAgeThreshold = currentDate.AddYears(-minAge);

                var filteredEmployees = await context.Employees
                    .Where(e => e.DateOfBirth >= minAgeThreshold && e.DateOfBirth <= maxAgeThreshold).ToListAsync();

                if (filteredEmployees.Count == 0)
                {
                    return NotFound();
                }

                var employeesDTO = new List<EmployeeDTO>();
                foreach (var employee in filteredEmployees)
                {
                    employeesDTO.Add(new EmployeeDTO()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        DateOfBirth = employee.DateOfBirth,
                        Salary = employee.Salary,
                        Department = employee.Department
                    });
                }
                return Ok(employeesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("SalaryRange")]
        public async Task<IActionResult> GetEmployeesBySalaryRange([FromQuery] int minAmount, [FromQuery] int maxAmount)
        {
            try
            {
                if (minAmount < 10000 || maxAmount > 100000 || minAmount > maxAmount)
                {
                    return BadRequest("Invalid Salary Range");
                }

                var filteredEmployees = await context.Employees.Where(e => e.Salary >= minAmount && e.Salary <= maxAmount).ToListAsync();

                if(filteredEmployees.Count == 0)
                {
                    return NotFound();
                }

                var employeesDTO = new List<EmployeeDTO>();
                foreach (var employee in filteredEmployees)
                {
                    employeesDTO.Add(new EmployeeDTO()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        DateOfBirth = employee.DateOfBirth,
                        Salary = employee.Salary,
                        Department = employee.Department
                    });
                }
                return Ok(employeesDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("emailDomain/{domain}")]
        public async Task<IActionResult> GetEmployeesByEmailDomain([FromRoute] string domain)
        {
            var employees = await context.Employees.Where(e => e.Email.Contains(domain)).ToListAsync();
            if(employees.Count == 0)
            {
                return NotFound();
            }

            var employeesDTO = new List<EmployeeDTO>();
            foreach (var employee in employees)
            {
                employeesDTO.Add(new EmployeeDTO()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateOfBirth = employee.DateOfBirth,
                    Salary = employee.Salary,
                    Department = employee.Department
                });
            }
            return Ok(employeesDTO);
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] AddEmployeeDTORequest request)
        { 
            new CustomValidation().ValidateAndThrow(request);
            var EmployeeDomainModel = new Employee()
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth.Date,
                Salary = request.Salary,
                Department = request.Department
            };

            await context.Employees.AddAsync(EmployeeDomainModel);
            await context.SaveChangesAsync();

            var employeeDTO = new EmployeeDTO()
            {
                Id = EmployeeDomainModel.Id,
                Name = EmployeeDomainModel.Name,
                Email = EmployeeDomainModel.Email,
                Salary = EmployeeDomainModel.Salary,
                DateOfBirth = EmployeeDomainModel.DateOfBirth,
                Department = EmployeeDomainModel.Department
            };

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDTO.Id }, employeeDTO);  // this 'id' is belongs to GetEmployeeById method
        }





        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] UpdateEmployeeDTORequest request, [FromRoute] int id)
        {
            new CustomValidationUpdate().ValidateAndThrow(request);
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Salary = request.Salary;
            employee.Department = request.Department;
            employee.DateOfBirth = request.DateOfBirth.Date;

            var employeeDTO = new EmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Department = employee.Department
            };

            await context.SaveChangesAsync();
            return Ok(employeeDTO);
        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee([FromRoute] int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return Ok(String.Format("Employee with Id = {0} is Deleted Successfully", employee.Id));
        } 
    }
}
