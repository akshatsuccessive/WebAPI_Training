using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeFK_API.Data.Base
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();

        Task<Department> GetDepartmentById(int id);

        Task<Department> AddDepartment(AddDepartmentDTO request);

        Task<Department> UpdateDepartment(int id, UpdateDepartmentDTO request);

        Task<Department> DeleteDepartment(int id);
    }
}
