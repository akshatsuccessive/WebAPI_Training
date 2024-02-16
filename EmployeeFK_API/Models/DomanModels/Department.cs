using System.Text.Json.Serialization;

namespace EmployeeFK_API.Models.DomanModels
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [JsonIgnore]
        public virtual IList<Employee> Employees { get; set; }
    }
}
