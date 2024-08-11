using MISA.Web.Core.Entities;
using MISA.Web.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace MISA.Web.Infrastructure.Repository
{
    public class EmployeeRepository :BaseRepository<Employee>, IEmployeeRepository
    {
        
        public bool CheckEmpCode(string employeeCode)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //check trung
                var sqlCheck = "select EmployeeCode from Employee where EmployeeCode = @EmployeeCode";
                var dynamic = new DynamicParameters();
                dynamic.Add("@EmployeeCode", employeeCode);
                var res = _mySqlConnection.QueryFirstOrDefault<string>(sqlCheck, param: dynamic);
                if (res != null)
                    return true;
                return false;
            }          
        }

        public bool CheckEmpId(Guid employeeId)
        {
            using(_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var existingEmployee = _mySqlConnection.QueryFirstOrDefault<Employee>("SELECT * FROM Employee WHERE EmployeeId = @Id", new { Id = employeeId });
                if (existingEmployee == null)
                    return true;
                return false;
            }
        }

        public int Delete(Guid employeeId)
        {
            return base.Delete(employeeId);
        }

        public IEnumerable<Employee> GetAll()
        {
            return GetAll<Employee>();
        }

        public Employee GetbyId(Guid enployeeId)
        {
            return GetById(enployeeId);
        }

        public int Insert(Employee employee)
        {
            return base.Insert(employee);
        }

        public int Update(Employee employee, Guid employeeId)
        {
            return base.Update(employee, employeeId);
        }
    }
}
