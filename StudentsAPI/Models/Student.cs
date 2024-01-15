namespace StudentsAPI.Models
{
    public class Student
    {
        // properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Course { get; set; }
    }
}
