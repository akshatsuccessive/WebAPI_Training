﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                //Id = context.Students.IsNullOrEmpty() ? 1 : context.Students.ToList().Count + 1,
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

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = await context.Students.FindAsync(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound("Invalid Student");
            }
        }


        // update student
        [HttpPut]
        [Route("{id}")]   // by default [Route("{id:int"})]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, UpdateStudentRequest updateStudentRequest)  // updateStudentRequest contains the fields which will be given by the user
        {
            var student = await context.Students.FindAsync(id);
            long currentPhone = student.Phone;

            if (student != null)
            {
                if (updateStudentRequest.Name.Length != 0)
                {
                    student.Name = updateStudentRequest.Name;
                }
                if (updateStudentRequest.Email.Length != 0)
                {
                    student.Email = updateStudentRequest.Email;
                }
                if (updateStudentRequest.Course.Length != 0)
                {
                    student.Course = updateStudentRequest.Course;
                }


                if (updateStudentRequest.Phone == 0)
                {
                    student.Phone = currentPhone;
                }
                else
                {
                    student.Phone = updateStudentRequest.Phone;
                }

                await context.SaveChangesAsync();

                return Ok(student);     // returning the response back
            }
            else
            {
                return NotFound();
            }
        }


        // delete a student
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await context.Students.FindAsync(id);
            if(student != null)
            {
                // delete request
                context.Remove(student);
                await context.SaveChangesAsync();
                return Ok(student);
            }
            return NotFound();
        }
    }
}