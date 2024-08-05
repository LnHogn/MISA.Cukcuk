using MISA.Web.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(Guid employeeId);
        int Insert(Employee employee);
        int Update(Employee employee, Guid employeeId);
        int Delete(Guid employeeId);
    }
}
