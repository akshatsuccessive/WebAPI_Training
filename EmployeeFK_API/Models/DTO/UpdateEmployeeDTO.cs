namespace EmployeeFK_API.Models.DTO
{
    public class UpdateEmployeeDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public int DepartmentId { get; set; }
    }
}