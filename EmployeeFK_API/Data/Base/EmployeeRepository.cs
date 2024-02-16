using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFK_API.Data.Base
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await context.Employees.ToListAsync();
            return employees;
        }


        public async Task<Employee> GetEmployeeById(Guid id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            
            return employee;
        }


        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await context.Employees.Where(e => e.DepartmentId == departmentId).ToListAsync();
            
            return employees;
        }

        public async Task<Employee> AddEmployee(AddEmployeeDTO request)
        {
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
            return newEmployee;
        }

        public async Task<Employee> UpdateEmployee(Guid id, UpdateEmployeeDTO request)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if(employee == null)
            {
                return null;
            }
            
            employee.EmployeeId = id;
            employee.Name = request.Name;
            employee.Address = request.Address;
            employee.Age = request.Age;
            employee.Salary = request.Salary;
            employee.DepartmentId = request.DepartmentId;

            await context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployee(Guid id)
        {
            var employee = await context.Employees.FindAsync(id);
            if(employee == null)
            {
                return null;
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return employee;
        }

    }
}
