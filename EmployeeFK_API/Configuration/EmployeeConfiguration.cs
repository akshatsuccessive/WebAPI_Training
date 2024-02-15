using EmployeeFK_API.Models.DomanModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeFK_API.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.EmployeeId);

            builder.HasOne<Department>(e => e.Departments)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);
        }
    }
}
