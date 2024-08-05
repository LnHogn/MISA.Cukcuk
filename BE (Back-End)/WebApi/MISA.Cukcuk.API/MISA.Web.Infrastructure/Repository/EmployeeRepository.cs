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
        public int Delete(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            //khoi tao ket noi database
            var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            //lay du lieu
            var sqlCommand = "select * from Employee";
            var employees = sqlConnection.Query<Employee>(sql: sqlCommand);
            //tra ket qua
            return employees;
        }

        public Employee GetById(Guid EmployeeId)
        {
            //khoi tao ket noi database
            var connectionString = "Host=8.222.228.150; Port=3306; Database = HAUI_2021600453_LamNguyenHong; User id=manhnv; Password= 12345678";
            var sqlConnection = new MySqlConnection(connectionString);
            //lay du lieu
            var sqlCommand = "select * from Employee where EmployeeId = @EmployeeId";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", EmployeeId);
            //truy van du lieu trong database
            var employee = sqlConnection.QueryFirstOrDefault<Employee>(sql: sqlCommand, param: dynamicParameters);
            //tra ket qua
            return employee;
        }

        public int Insert(Employee employee)
        {
            // Khởi tạo kết nối cơ sở dữ liệu
            var connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
            using var sqlConnection = new MySqlConnection(connectionString);

            // Thêm nhân viên mới vào cơ sở dữ liệu
            var sqlCommand = @"
                        INSERT INTO Employee (EmployeeId, EmployeeCode, FullName, DateOfBirth, Gender, IdentityNumber, IdentityDate, IdentityPlace, Email, PhoneNumber, Address, LandlineNumber, BankAccount, BankName, Branch, PositionId, DepartmentId, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
                        VALUES (@EmployeeId, @EmployeeCode, @FullName, @DateOfBirth, @Gender, @IdentityNumber, @IdentityDate, @IdentityPlace, @Email, @PhoneNumber, @Address, @LandlineNumber, @BankAccount, @BankName, @Branch, @PositionId, @DepartmentId, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";

            employee.EmployeeId = Guid.NewGuid();
            employee.CreatedDate = DateTime.Now;
            employee.ModifiedDate = DateTime.Now;
            employee.CreatedBy = "lam nguyen hong";
            employee.ModifiedBy = "lam nguyen hong";

            var res = sqlConnection.Execute(sqlCommand, param: employee);
            return res;
        }

        public int Update(Employee employee, Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
