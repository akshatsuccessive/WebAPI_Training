﻿namespace StudentsAPI.Models
{
    public class UpdateStudentRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Course { get; set; }
    }
}
