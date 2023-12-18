namespace WebAPITraining.Models
{
    public class College
    {
        public static IList<Student> Students = new List<Student>()
        {
            new Student() { Id = 1, Name = "Akshat", Age = 21, Department = "IT" },
            new Student() { Id = 2, Name = "Bohra", Age = 23, Department = "CSE" },
            new Student() { Id = 3, Name = "Govind", Age = 22, Department = "SE" },
            new Student() { Id = 4, Name = "Yash", Age = 20, Department = "ECE" },
        };
    }
}
