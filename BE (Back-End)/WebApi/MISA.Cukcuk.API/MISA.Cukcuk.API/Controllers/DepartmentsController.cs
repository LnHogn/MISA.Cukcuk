using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;

namespace MISA.Cukcuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
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
            //khoi tao ket noi database
            var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            //lay du lieu
            var sqlCommand = "select * from Department";
            var departments = sqlConnection.Query<object>(sql: sqlCommand);
            //tra ket qua
            return Ok(departments);
        }
    }
}
