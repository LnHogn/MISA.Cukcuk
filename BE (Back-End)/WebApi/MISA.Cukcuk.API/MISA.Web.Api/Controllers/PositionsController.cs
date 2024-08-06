using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Interfaces.Infrastructure;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : Controller
    {
        IEmployeeRepository _employeeRepository;
        public PositionsController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// lay danh sach toan bo vi tri
        /// </summary>
        /// <returns>
        /// 200 danh sach vi tri
        /// 204 khong co du kieu
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpGet]
        public IActionResult Get()
        {
            var departments = _employeeRepository.GetPositions();
            return Ok(departments);
        }
    }
}
