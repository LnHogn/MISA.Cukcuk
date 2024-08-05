using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        public int InsertService(Employee employee)
        {
            //validate data
            //validate data
            var errorData = new Dictionary<string, string>();
            var errorMsg = new List<string>();
            //mã nhân viên không được để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {              
                throw new MISAValidateException("Mã nhân viên không được phép để trống");
            }
            //check ma nv ko dc trung
            //if (CheckEmpCode(employee.EmployeeCode))
            //{
            //    errorData.Add("EmployeeCode", "Mã nhân viên không được trùng lặp");
            //    errorMsg.Add("Mã nhân viên không được trùng lặp");
            //}
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
            return 1;
        }

        public int UpdateService(Employee employee, Guid employeeId)
        {
            throw new NotImplementedException();
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
        //private bool CheckEmpCode(string employeeCode)
        //{
        //    // Khởi tạo kết nối cơ sở dữ liệu
        //    var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
        //    using var sqlConnection = new MySqlConnection(connectionString);
        //    //check trung
        //    var sqlCheck = "select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
        //    var dynamic = new DynamicParameters();
        //    dynamic.Add("@EmployeeCode", employeeCode);
        //    var res = sqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: dynamic);
        //    if (res != null)
        //        return true;
        //    return false;
        //}
    }
}
