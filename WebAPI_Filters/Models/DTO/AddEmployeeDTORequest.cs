namespace Employees_API2.Models.DTO
{
    public class AddEmployeeDTORequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
    }
}
