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
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly string _connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
        MySqlConnection _mySqlConnection;
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
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //lay du lieu
                var sqlCommand = "select * from Employee";
                var employees = _mySqlConnection.Query<Employee>(sql: sqlCommand);
                //tra ket qua
                return employees;
            }
        }

        public Employee GetById(Guid EmployeeId)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //lay du lieu
                var sqlCommand = "select * from Employee where EmployeeId = @EmployeeId";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeId", EmployeeId);
                //truy van du lieu trong database
                var employee = _mySqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: dynamicParameters);
                //tra ket qua
                return employee;
            }
        }


        public IEnumerable<Positions> GetPositions()
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //lay du lieu
                var sqlCommand = "select * from Positions";
                var positions = _mySqlConnection.Query<Positions>(sql: sqlCommand);
                //tra ket qua
                return positions;
            }
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
