using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Services;
using MISA.Web.Infrastructure.Repository;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            //lay du lieu:
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var employees = employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("EmployeeId")]
        public IActionResult GetByID(Guid EmployeeId)
        {
            //lay du lieu
            EmployeeRepository EmployeeRepository = new EmployeeRepository();
            var employees = EmployeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                //validate du lieu
                EmployeeService employeeService = new EmployeeService();
                var res = employeeService.InsertService(employee);
                //them du lieu vao database
                EmployeeRepository employeeRepository = new EmployeeRepository();
                var res2 = employeeRepository.Insert(employee);  
                return Ok(res);                

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = employee,
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            return Ok();
        }
    }
}
