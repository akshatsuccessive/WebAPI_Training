using EmployeeFK_API.Configuration;
using EmployeeFK_API.Models.DomanModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFK_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
