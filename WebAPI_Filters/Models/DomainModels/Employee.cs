using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Employees_API2.Models.DomainModels
{
    public class Employee
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
    }
}
