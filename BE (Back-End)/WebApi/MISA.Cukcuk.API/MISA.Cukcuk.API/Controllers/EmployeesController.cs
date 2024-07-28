using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;

namespace MISA.Cukcuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// lay danh sach toan bo nhan vien
        /// </summary>
        /// <returns>
        /// 200 danh sach nhan vien
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
            var sqlCommand = "select * from Employee";
            var employees = sqlConnection.Query<object>(sql:sqlCommand) ;
            //tra ket qua
            return Ok(employees);
        }

        /// <summary>
        /// lay 1 nhan vien theo id
        /// </summary>
        /// <returns>
        /// 200 danh sach nhan vien
        /// 204 khong co du kieu
        /// </returns>
        /// created by LamNguyenHong(29/07/2024)
        [HttpGet("EmployeeId")]
        public IActionResult GetByID(Guid EmployeeId)
        {
            //khoi tao ket noi database
            var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            //lay du lieu
            var sqlCommand = $"select * from Employee where EmployeeId = @EmployeeId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", EmployeeId);
            var employee = sqlConnection.QueryFirstOrDefault<object>(sql: sqlCommand,param:dynamicParameters);
            //tra ket qua
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Post([FromBody]string? ten, int tuoi)
        {
            return Ok($"du lieu vua post la: {ten} ; {tuoi} ");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("api put");
        }
        [HttpDelete("EmployeeId")]
        public IActionResult Delete(int id)
        {
            return Ok($"api delete by id: {id}");
        }

    }
}
