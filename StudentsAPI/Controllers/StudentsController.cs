using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Data;
using StudentsAPI.Models;

namespace StudentsAPI.Controllers
{
    [ApiController]             // annotating, describing this is API controller and not a MVC controller
    [Route("api/students")]     // or [Route("api/[controller]")
    public class StudentsController : Controller
    {
        private readonly StudentsAPI_DbContext context;
        public StudentsController(StudentsAPI_DbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(context.Students.ToList());    // Students is Table name
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentRequest addStudentRequest)
        {
            var student = new Student()     // this is what goes in database
            {
                Id = context.Students.ToList().Count + 1,
                Name = addStudentRequest.Name,
                Email = addStudentRequest.Email,
                Phone = addStudentRequest.Phone,
                Course = addStudentRequest.Course
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            // return the request(student is request)
            return Ok(student);
        }
    }
}
