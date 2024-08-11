using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Core.Services;
using MISA.Web.Infrastructure.Repository;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase
    {
        #region Fields
        IBaseRepository<MISAEntity> _baseRepository;
        IBaseService<MISAEntity> _baseService;
        #endregion

        #region Constructor
        public MISABaseController(IBaseRepository<MISAEntity> baseRepository,
        IBaseService<MISAEntity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// lay danh sach toan bo du lieu
        /// </summary>
        /// <returns>
        /// 200 danh sach du lieu
        /// 204 khong co du lieu
        /// 400 loi nghiep vu
        /// 500 exception
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var data = _baseRepository.GetAll();
                return Ok(data);
            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.ErrorException,
                    data = ex.InnerException,
                };
                return StatusCode(500,response);
            }
        }
        /// <summary>
        /// them du lieu
        /// </summary>
        /// <returns>
        /// 201 them thanh cong      
        /// 400 loi nghiep vu
        /// 500 exception
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            try
            {
                //validate du lieu
                var res = _baseService.InsertService(entity);
                return StatusCode(201, res);

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.ErrorException,
                    data = ex.InnerException,
                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// sua du lieu
        /// </summary>
        /// <returns>
        /// 200 - Cập nhật thành công
        /// 400 - Yêu cầu không hợp lệ
        /// 404 - Không tìm thấy nhân viên
        /// 500 - exception
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, MISAEntity entity)
        {
            try
            {
                var res = _baseService.UpdateService(entity,id);
                return Ok(res);
            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.ErrorException,
                    data = ex.InnerException,
                };
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// xoa du lieu
        /// </summary>
        /// <returns>
        /// 200 xoa thanh cong     
        /// 400 loi nghiep vu
        /// 500 exception
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //validate du lieu
                var res = _baseRepository.Delete(id);
                return Ok( res);

            }
            catch (MISAValidateException ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = ex.Message,
                    data = ex.Data,
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    DevMsg = ex.Message,
                    userMsg = Core.Resources.ResourceVN.ErrorException,
                    data = ex.InnerException,
                };
                return StatusCode(500, response);
            }
        }
        #endregion
    }
}
