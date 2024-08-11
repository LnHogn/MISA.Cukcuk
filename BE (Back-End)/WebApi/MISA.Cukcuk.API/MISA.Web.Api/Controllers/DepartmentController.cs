using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Infrastructure.Repository;
using MISA.Web.Core.Interfaces.Services;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {

        IDepartmentRepository _departmentRepository;
        IDepartmentService _departmentService;
        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentService departmentService)
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
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

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department department)
        {
            try
            {
                //validate du lieu
                var res = _departmentService.InsertService(department);
                return StatusCode(201, res);

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = department,
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(Guid id, [FromBody] Department department)
        {
            try
            {
                var res = _departmentService.UpdateService(department, id);
                return Ok(res);
            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = department,
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            try
            {
                //validate du lieu
                var res = _departmentRepository.Delete(id);
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
