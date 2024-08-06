using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Core.Services;
using MISA.Web.Infrastructure.Repository;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {

        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //lay du lieu:
            var employees = _employeeRepository.GetAll();
            return Ok(employees);
        }

        [HttpGet("EmployeeId")]
        public IActionResult GetByID(Guid EmployeeId)
        {
            //lay du lieu
            var employees = _employeeRepository.GetById(EmployeeId);
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                //validate du lieu
                var res = _employeeService.InsertService(employee);
                return StatusCode(201,res);                

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
            try
            {
                //validate du lieu
                var res = _employeeRepository.Delete(id);
                return StatusCode(201, res);

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = "",
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
