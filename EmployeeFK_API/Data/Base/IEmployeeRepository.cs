using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeFK_API.Data.Base
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<Employee> GetEmployeeById(Guid id);

        Task<IEnumerable<Employee>> GetEmployeesByDepartment(int departmentId);

        Task<Employee> AddEmployee(AddEmployeeDTO request);

        Task<Employee> UpdateEmployee(Guid id, UpdateEmployeeDTO request);

        Task<Employee> DeleteEmployee(Guid id);
    }
}
