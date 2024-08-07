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

        public int Delete(Guid employeeId)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = "DELETE FROM Employee WHERE EmployeeId = @Id";
                var res = _mySqlConnection.Execute(sqlCommand, new { EmployeeId = employeeId });
                return res;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return GetAll();
        }

        public Employee GetById(Guid EmployeeId)
        {
            return GetById(EmployeeId);
        }      

        public int Insert(Employee employee)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {

                // Thêm nhân viên mới vào cơ sở dữ liệu
                var sqlCommand = @"
                        INSERT INTO Employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender, IdentityNumber, IdentityDate, IdentityPlace, Email, PhoneNumber, Address, LandlineNumber, BankAccount, BankName, Branch, PositionId, DepartmentId, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
                        VALUES (@EmployeeId, @EmployeeCode, @FullName, @DateOfBirth, @Gender, @IdentityNumber, @IdentityDate, @IdentityPlace, @Email, @PhoneNumber, @Address, @LandlineNumber, @BankAccount, @BankName, @Branch, @PositionId, @DepartmentId, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";

                employee.EmployeeId = Guid.NewGuid();
                employee.CreatedDate = DateTime.Now;
                employee.ModifiedDate = DateTime.Now;
                employee.CreatedBy = "lam nguyen hong";
                employee.ModifiedBy = "lam nguyen hong";

                var res = _mySqlConnection.Execute(sqlCommand, param: employee);
                return res;
            }
        }

        public int Update(Employee employee, Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
