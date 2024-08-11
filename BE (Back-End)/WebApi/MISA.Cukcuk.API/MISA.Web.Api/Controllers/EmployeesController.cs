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
    public class EmployeesController : MISABaseController<Employee>
    {

        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService):base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        
        [HttpGet("{EmployeeId}")]
        public IActionResult GetByID(Guid EmployeeId)
        {
            //lay du lieu
            var employees = _employeeRepository.GetById(EmployeeId);
            return Ok(employees);
        }

    }
}
