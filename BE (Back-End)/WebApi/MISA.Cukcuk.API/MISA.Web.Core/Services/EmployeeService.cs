using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Infrastructure;
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

        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int InsertService(Employee employee)
        {
            //validate data
            //mã nhân viên không được để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {              
                throw new MISAValidateException("Mã nhân viên không được phép để trống");
            }
            //check ma nv ko dc trung
            var isDuplidate = _employeeRepository.CheckEmpCode(employee.EmployeeCode);
            if (isDuplidate)
            {
                throw new MISAValidateException("Mã nhân viên không được phép trùng");
            }
            //họ tên không được để trống
            if (string.IsNullOrEmpty(employee.FullName))
            {
                throw new MISAValidateException("Tên nhân viên không được phép để trống");
            }
            //so dien thoai
            if (string.IsNullOrEmpty(employee.PhoneNumber))
            {
                throw new MISAValidateException("Sdt nhân viên không được phép để trống");
            }
            //so cccd
            if (string.IsNullOrEmpty(employee.IdentityNumber))
            {
                throw new MISAValidateException("Số cccd nhân viên không được phép để trống");
            }
            //email
            if (!IsValidEmail(employee.Email))
            {
                throw new MISAValidateException("Email không đúng định dạng");
            }
            //them moi vao database
            var res = _employeeRepository.Insert(employee);
            return res;
        }

        public int UpdateService(Employee employee, Guid employeeId)
        {
            var isDuplicateId = _employeeRepository.CheckEmpId(employeeId);
            if (isDuplicateId)
            {
                throw new MISAValidateException("nhân viên không tồn tại");
            }
            var res = _employeeRepository.Update(employee, employeeId);
            return res;
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
    }
}
