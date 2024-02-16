using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFK_API.Data.Base
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var departments = await context.Departments.ToListAsync();
            return departments;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await context.Departments.FindAsync(id);
            if(department == null)
            {
                return null;
            }
            return department;
        }


        public async Task<Department> AddDepartment(AddDepartmentDTO request)
        {
            var newDepartment = new Department()
            {
                DepartmentName = request.DepartmentName
            };

            await context.Departments.AddAsync(newDepartment);
            await context.SaveChangesAsync();
            return newDepartment;
        }

        public async Task<Department> UpdateDepartment(int id, UpdateDepartmentDTO request)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

            if(department == null)
            {
                return null;
            }
            department.DepartmentName = request.DepartmentName;

            await context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> DeleteDepartment(int id)
        {
            var department = await context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
            if(department == null)
            {
                return null;
            }
            context.Remove(department);
            await context.SaveChangesAsync();
            return department;
        }
    }
}
