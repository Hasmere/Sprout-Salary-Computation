using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Factories;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApplicationDbContext _context; // Inject ApplicationDbContext

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Employee
                .Where(e => e.IsDeleted == false)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Birthdate = e.Birthdate.ToString("yyyy-MM-dd"), // Format the date
                    Tin = e.Tin,
                    TypeId = e.EmployeeTypeId
                })
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Employee
            .Where(e => e.Id == id && e.IsDeleted == false)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                Birthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                Tin = e.Tin,
                TypeId = e.EmployeeTypeId
            })
            .FirstOrDefaultAsync();
            
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await _context.Employee
                .Where(e => e.Id == input.Id && e.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate;
            item.EmployeeTypeId = input.TypeId;

            try
            {
                await _context.SaveChangesAsync(); 
                return Ok(item);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Concurrency exception occurred.");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

            var newEmployee = new Employee
            {
                Birthdate = input.Birthdate,
                FullName = input.FullName,
                Tin = input.Tin,
                EmployeeTypeId = input.TypeId,
                IsDeleted = false
            };

            _context.Employee.Add(newEmployee);

            try
            {
                await _context.SaveChangesAsync();
                return Created($"/api/employees/{newEmployee.Id}", newEmployee.Id);
            }
            catch (Exception)
            {
                return BadRequest("Failed to create employee.");
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Employee
                .Where(e => e.Id == id && e.IsDeleted == false)
                .FirstOrDefaultAsync();
            
            if (result == null) return NotFound();

            result.IsDeleted = true;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Concurrency exception occurred.");
            }
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate/{absentDays}/{workedDays}")]
        public async Task<IActionResult> Calculate(int id,decimal absentDays,decimal workedDays)
        {
            var result = await _context.Employee
                .Where(e => e.Id == id && e.IsDeleted == false)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FullName = e.FullName,
                    Birthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                    Tin = e.Tin,
                    TypeId = e.EmployeeTypeId
                })
                .FirstOrDefaultAsync();
            
            if (result == null) return NotFound();
            
            var employeeType = (EmployeeType)result.TypeId;
            
            var calculatorFactory = new SalaryCalculatorFactory();
            
            var salaryCalculator = calculatorFactory.Create(employeeType);
            
            var salary = salaryCalculator.CalculateSalary(absentDays, workedDays);

            return Ok(salary);
        }

    }
}
