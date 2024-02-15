using EmployeeFK_API.Models.DomanModels;
using System.Text.Json.Serialization;

namespace EmployeeFK_API.Models.DTO
{
    public class AddEmployeeDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
