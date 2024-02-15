using System.Text.Json.Serialization;

namespace EmployeeFK_API.Models.DomanModels
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public Guid DepartmentId { get; set; }
        [JsonIgnore]
        public Department Departments { get; set; }
    }
}
