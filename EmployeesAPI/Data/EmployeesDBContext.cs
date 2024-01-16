using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Models;

namespace EmployeesAPI.Data
{
    public class EmployeesDBContext : DbContext
    {
        public EmployeesDBContext(DbContextOptions options) : base(options) 
        {
            
        }

        // table in database
        public DbSet<Employee> Employees { get; set; }
    }
}
