﻿using EmployeeFK_API.Data;
using EmployeeFK_API.Data.Base;
using EmployeeFK_API.Models.DomanModels;
using EmployeeFK_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeFK_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository empRepo;
        public EmployeeController(IEmployeeRepository empRepo)
        {
            this.empRepo = empRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await empRepo.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            var employee = await empRepo.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartment([FromRoute] int departmentId)
        {
            try
            {
                var employees = await empRepo.GetEmployeesByDepartment(departmentId);
                if (employees.Count() == 0)
                {
                    return NotFound("No Employees Found");
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDTO request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var newEmployee = await empRepo.AddEmployee(request);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.EmployeeId }, newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeDTO request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var employee = await empRepo.UpdateEmployee(id, request);   
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await empRepo.DeleteEmployee(id);
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
