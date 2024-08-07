using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Entities;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {

        IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        /// <summary>
        /// lay danh sach toan bo phong ban
        /// </summary>
        /// <returns>
        /// 200 danh sach phong ban
        /// 204 khong co du kieu
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpGet]
        public IActionResult Get() 
        { 
            var departments = _departmentRepository.GetAll();
            return Ok(departments);
        }
    }
}
