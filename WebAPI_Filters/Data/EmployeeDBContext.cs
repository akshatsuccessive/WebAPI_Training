using Employees_API2.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Employees_API2.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set;}
    }
}
