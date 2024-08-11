using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Infrastructure.Repository;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : Controller
    {
        IPositionsRepository _positionsRepository;
        IPositionsService _positionsService;

        public PositionsController(IPositionsRepository positionsRepository, IPositionsService positionsService)
        {
            _positionsRepository = positionsRepository;
            _positionsService = positionsService;
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
            var departments = _positionsRepository.GetAll();
            return Ok(departments);
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Positions positions)
        {
            try
            {
                //validate du lieu
                var res = _positionsService.InsertService(positions);
                return StatusCode(201, res);

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = positions,
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(Guid id, [FromBody] Positions positions)
        {
            try
            {
                var res = _positionsService.UpdateService(positions, id);
                return Ok(res);
            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = positions,
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
                var res = _positionsRepository.Delete(id);
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
