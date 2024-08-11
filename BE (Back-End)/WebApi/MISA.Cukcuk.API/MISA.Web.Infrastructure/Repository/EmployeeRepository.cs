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
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
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

                employee.EmployeeId = employeeId;
                employee.ModifiedDate = DateTime.Now;

                var res = _mySqlConnection.Execute(sqlCommand, param: employee);

                return res;
            }
        }
    }
}
