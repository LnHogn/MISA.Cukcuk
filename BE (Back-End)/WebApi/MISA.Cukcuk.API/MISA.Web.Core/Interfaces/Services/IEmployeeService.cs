using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web.Core.Entities;

namespace MISA.Web.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// them moi du lieu
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// created by lam nguyen hong (5/8/2024)
        int InsertService(Employee employee);

        /// <summary>
        /// sua du lieu
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// created by lam nguyen hong (5/8/2024)
        int UpdateService(Employee employee, Guid employeeId);

    }
}
