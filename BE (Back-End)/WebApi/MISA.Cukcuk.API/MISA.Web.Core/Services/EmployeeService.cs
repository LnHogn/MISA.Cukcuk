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
    public class EmployeeService :BaseService<Employee>, IEmployeeService
    {

        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

        protected override void ValidateEmployee(Employee employee)
        {
            //check ma nv ko dc trung
                var isDuplidate = _employeeRepository.CheckEmpCode(employee.EmployeeCode);
            if (isDuplidate)
            {
                throw new MISAValidateException(Core.Resources.ResourceVN.EmployeeCodeNotDuplicate);
            }
            //email
            if (!IsValidEmail(employee.Email))
            {
                throw new MISAValidateException(Core.Resources.ResourceVN.EmailNotFormat);
            }
        }
    }
}
