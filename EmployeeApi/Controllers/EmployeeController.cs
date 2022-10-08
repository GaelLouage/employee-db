using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Infrastructuur.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

       
        [HttpGet]
        [Route("Employees")]
        [ProducesResponseType(200, Type = typeof(EmployeeDto))] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            return Ok(await _employeeService.GetEmployeesAsync());
        }


        // by id
        [HttpGet("Employee/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            // check if id isnan
            return Ok(await _employeeService.GetEmployeeByIdAsync(id));
        }
        [HttpPost]
        [Route("Employee/Create")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> CreateEmployeec(EmployeeEntity employee)
        {
        
            return Ok(await _employeeService.CreateEmployeeAsync(employee));
        }
        // update employee
        [HttpPost("Employee/Update/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> UpdateEmployee(EmployeeEntity employee, int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id) ?? null;
            if(emp == null)
            {
                return NotFound();
            }
            emp.AddressId = employee.AddressId;
            emp.Name = employee.Name;

            return Ok(await _employeeService.UpdateEmployeeAsync(emp));
        }
        // delete employee
        [HttpPost("Employee/Delete/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id) ?? null;
            if(emp == null)
            {
                return NotFound();
            }
            return Ok(await _employeeService.DeleteEmployeeAsync(emp));
        }


        [HttpGet("EmployeesOnSameAddress/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> GetAllEmployeesOnSameAddress(int id)
        {
            return Ok(await _employeeService.GetAllEmployeesWithSameAddress(id));
        }

    }
}
