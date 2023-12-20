using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPITraining.Models;

namespace WebAPITraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetStudentList()
        {
            return Ok(College.Students);
        }

        [HttpGet("id:int", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentList(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = College.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("name:alpha", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var student = College.Students.FirstOrDefault(x => x.Name == name);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(student);
            }
            if (student.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            student.Id = College.Students.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            College.Students.Add(student);

            return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var student = College.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound(student);
            }
            College.Students.Remove(student);
            return Ok(student);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var existingStudent = College.Students.FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Department = updatedStudent.Department;

            return Ok(existingStudent);
        }
    }
}
