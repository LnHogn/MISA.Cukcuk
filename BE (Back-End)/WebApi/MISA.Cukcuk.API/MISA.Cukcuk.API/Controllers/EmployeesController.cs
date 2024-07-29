using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA.Cukcuk.API.Model;

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
            try
            {
            //khoi tao ket noi database
            var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            //lay du lieu
            var sqlCommand = "select * from Employee";
            var employees = sqlConnection.Query<Employee>(sql: sqlCommand);
            //tra ket qua
            return Ok(employees);
            }
            catch (Exception ex)
            {
                var err = new ErrorService();
                err.devMsg = ex.Message;
                err.userMsg = "có lỗi xảy ra";
                return StatusCode(500, err);
            }
            
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
            try
            {
                //khoi tao ket noi database
                var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
                var sqlConnection = new MySqlConnection(connectionString);
                //lay du lieu
                var sqlCommand = "select * from Employee where EmployeeId = @EmployeeId";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeId", EmployeeId);
                var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: dynamicParameters);
                //tra ket qua
                return Ok(employee);
            }
            catch(Exception ex)
            {
                var err = new ErrorService();
                err.devMsg = ex.Message;
                err.userMsg = "có lỗi xảy ra";
                return StatusCode(500, err);
            }
            
        }

        /// <summary>
        /// Thêm một nhân viên mới
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên</param>
        /// <returns>
        /// 201 - Nhân viên đã được tạo
        /// 400 - Yêu cầu không hợp lệ
        /// 500 - Lỗi máy chủ nội bộ
        /// </returns>
        /// created by LamNguyenHong (29/07/2024)

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                //validate data
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsg = new List<string>();
                //mã nhân viên không được để trống
                if(string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode","Mã nhân viên không được phép để trống");
                    errorMsg.Add("Mã nhân viên không được phép để trống");
                }
                //check ma nv ko dc trung
                if(CheckEmpCode(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", "Mã nhân viên không được trùng lặp");
                    errorMsg.Add("Mã nhân viên không được trùng lặp");
                }
                //họ tên không được để trống
                if(string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", "Tên nhân viên không được phép để trống");
                    errorMsg.Add("Tên nhân viên không được phép để trống");
                }
                //so dien thoai
                if (string.IsNullOrEmpty(employee.PhoneNumber))
                {
                    errorData.Add("PhoneNumber", "Sdt nhân viên không được phép để trống");
                    errorMsg.Add("Sdt nhân viên không được phép để trống");
                }
                //so cccd
                if (string.IsNullOrEmpty(employee.IdentityNumber))
                {
                    errorData.Add("IdentityNumber", "So cccd không được phép để trống");
                    errorMsg.Add("So cccd không được phép để trống");
                }
                //email
                if (!IsValidEmail(employee.Email))
                {
                    errorData.Add("Email", "Email không đúng định dạng");
                    errorMsg.Add("Email không đúng định dạng");
                }

                if(errorData.Count > 0)
                {
                    error.userMsg = "Dữ liệu đầu vào không hợp lệ";
                    error.data = errorData;
                    return BadRequest(error);
                }

                // Khởi tạo kết nối cơ sở dữ liệu
                var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
                using var sqlConnection = new MySqlConnection(connectionString);

                // Thêm nhân viên mới vào cơ sở dữ liệu
                var sqlCommand = @"
                        INSERT INTO Employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender, IdentityNumber, IdentityDate, IdentityPlace, Email, PhoneNumber, Address, LandlineNumber, BankAccount, BankName, Branch, PositionId, DepartmentId, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
                        VALUES (@EmployeeId, @EmployeeCode, @FullName, @DateOfBirth, @Gender, @IdentityNumber, @IdentityDate, @IdentityPlace, @Email, @PhoneNumber, @Address, @LandlineNumber, @BankAccount, @BankName, @Branch, @PositionId, @DepartmentId, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";

                employee.EmployeeId = Guid.NewGuid();
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = DateTime.Now;
                employee.CreatedBy = "lam nguyen hong";
                employee.ModifiedBy = "lam nguyen hong";

                var res = sqlConnection.Execute(sqlCommand,param: employee);
                if (res > 0)
                {
                    return StatusCode(201);
                }
                else
                {
                    return Ok(res);
                }
                
                
            }
            catch (Exception ex)
            {
                var err = new ErrorService();
                err.devMsg = ex.Message;
                err.userMsg = "Có lỗi xảy ra";
                return StatusCode(500, err);
            }
        }



        /// <summary>
        /// Cập nhật thông tin của một nhân viên
        /// </summary>
        /// <param name="id">ID của nhân viên</param>
        /// <param name="employee">Đối tượng nhân viên chứa thông tin cập nhật</param>
        /// <returns>
        /// 200 - Cập nhật thành công
        /// 400 - Yêu cầu không hợp lệ
        /// 404 - Không tìm thấy nhân viên
        /// 500 - Lỗi máy chủ nội bộ
        /// </returns>
        /// created by LamNguyenHong (29/07/2024)

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            try
            {
                //validate data
                var error = new ErrorService();
                var errorData = new Dictionary<string, string>();
                var errorMsg = new List<string>();
                //mã nhân viên không được để trống
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorData.Add("EmployeeCode", "Mã nhân viên không được phép để trống");
                    errorMsg.Add("Mã nhân viên không được phép để trống");
                }               
                //họ tên không được để trống
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    errorData.Add("FullName", "Tên nhân viên không được phép để trống");
                    errorMsg.Add("Tên nhân viên không được phép để trống");
                }
                //so dien thoai
                if (string.IsNullOrEmpty(employee.PhoneNumber))
                {
                    errorData.Add("PhoneNumber", "Sdt nhân viên không được phép để trống");
                    errorMsg.Add("Sdt nhân viên không được phép để trống");
                }
                //so cccd
                if (string.IsNullOrEmpty(employee.IdentityNumber))
                {
                    errorData.Add("IdentityNumber", "So cccd không được phép để trống");
                    errorMsg.Add("So cccd không được phép để trống");
                }
                //email
                if (!IsValidEmail(employee.Email))
                {
                    errorData.Add("Email", "Email không đúng định dạng");
                    errorMsg.Add("Email không đúng định dạng");
                }

                if (errorData.Count > 0)
                {
                    error.userMsg = "Dữ liệu đầu vào không hợp lệ";
                    error.data = errorData;
                    return BadRequest(error);
                }

                // Khởi tạo kết nối cơ sở dữ liệu
                var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
                using var sqlConnection = new MySqlConnection(connectionString);

                // Kiểm tra nếu nhân viên tồn tại
                var existingEmployee = sqlConnection.QueryFirstOrDefault<Employee>("SELECT * FROM Employee WHERE EmployeeId = @Id", new { Id = id });
                if (existingEmployee == null)
                {
                    return NotFound("Không tìm thấy nhân viên");
                }

                // Cập nhật thông tin nhân viên
                var sqlCommand = @"
            UPDATE Employee
            SET EmployeeCode = @EmployeeCode,
                FullName = @FullName,
                DateOfBirth = @DateOfBirth,
                Gender = @Gender,
                IdentityNumber = @IdentityNumber,
                IdentityDate = @IdentityDate,
                IdentityPlace = @IdentityPlace,
                Email = @Email,
                PhoneNumber = @PhoneNumber,
                Address = @Address,
                LandlineNumber = @LandlineNumber,
                BankAccount = @BankAccount,
                BankName = @BankName,
                Branch = @Branch,
                PositionId = @PositionId,
                DepartmentId = @DepartmentId,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE EmployeeId = @EmployeeId";

                employee.EmployeeId = id;
                employee.ModifiedDate = DateTime.Now;

                sqlConnection.Execute(sqlCommand,param: employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                var err = new ErrorService();
                err.devMsg = ex.Message;
                err.userMsg = "Có lỗi xảy ra";
                return StatusCode(500, err);
            }
        }


        /// <summary>
        /// Xóa một nhân viên theo ID
        /// </summary>
        /// <param name="id">ID của nhân viên</param>
        /// <returns>
        /// 200 - Xóa thành công
        /// 404 - Không tìm thấy nhân viên
        /// 500 - Lỗi máy chủ nội bộ
        /// </returns>
        /// created by LamNguyenHong (30/07/2024)
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("ID không hợp lệ");
                }

                // Khởi tạo kết nối cơ sở dữ liệu
                var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
                using var sqlConnection = new MySqlConnection(connectionString);

                // Kiểm tra nếu nhân viên tồn tại
                var existingEmployee = sqlConnection.QueryFirstOrDefault<Employee>("SELECT * FROM Employee WHERE EmployeeId = @Id", new { Id = id });
                if (existingEmployee == null)
                {
                    return NotFound("Không tìm thấy nhân viên");
                }

                // Xóa nhân viên
                var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @Id";
                sqlConnection.Execute(sqlCommand, new { Id = id });

                return Ok("Xóa nhân viên thành công");
            }
            catch (Exception ex)
            {
                var err = new ErrorService();
                err.devMsg = ex.Message;
                err.userMsg = "Có lỗi xảy ra";
                return StatusCode(500, err);
            }
        }


        //VALIDATE EMAIL
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        //CHECK TRÙNG MÃ NV
        private bool CheckEmpCode(string employeeCode)
        {
            // Khởi tạo kết nối cơ sở dữ liệu
            var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
            using var sqlConnection = new MySqlConnection(connectionString);
            //check trung
            var sqlCheck = "select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
            var dynamic = new DynamicParameters();
            dynamic.Add("@EmployeeCode", employeeCode);
            var res = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: dynamic);
            if (res != null)
                return true;
            return false;
        }
    }
}
