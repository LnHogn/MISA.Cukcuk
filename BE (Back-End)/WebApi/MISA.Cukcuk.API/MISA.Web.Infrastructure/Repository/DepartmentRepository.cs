using Dapper;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly string _connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
        MySqlConnection _mySqlConnection;
        public IEnumerable<Department> GetDepartment()
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //lay du lieu
                var sqlCommand = "select * from Department";
                var departments = _mySqlConnection.Query<Department>(sql: sqlCommand);
                //tra ket qua
                return departments;
            }
        }
    }
}
