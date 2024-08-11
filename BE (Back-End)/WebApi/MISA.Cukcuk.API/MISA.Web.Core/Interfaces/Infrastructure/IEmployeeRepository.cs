using MISA.Web.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        Employee GetById(Guid employeeId);
        

        bool CheckEmpCode(string employeeCode); 

        bool CheckEmpId(Guid employeeId);
    }
}
