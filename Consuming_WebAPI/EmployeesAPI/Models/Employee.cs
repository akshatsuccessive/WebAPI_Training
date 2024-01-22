using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EmployeesAPI.Models
{
    public class Employee
    {
        [ValidateNever]
        public int Id { get; internal set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
    }
}
