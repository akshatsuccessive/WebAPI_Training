using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI_WebApp.Models
{
    public class Employee
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
    }
}
