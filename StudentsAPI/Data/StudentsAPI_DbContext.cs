using Microsoft.EntityFrameworkCore;
using StudentsAPI.Models;

namespace StudentsAPI.Data
{
    public class StudentsAPI_DbContext : DbContext
    {
        public StudentsAPI_DbContext(DbContextOptions options) : base(options)
        {
            
        }

        // create properties which will act as a table in EF database
        public DbSet<Student> Students { get; set; }
    }
}
