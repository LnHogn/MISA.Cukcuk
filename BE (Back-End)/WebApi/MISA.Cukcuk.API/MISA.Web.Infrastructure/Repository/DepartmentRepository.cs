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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public int Delete(Guid departmentId)
        {
            return base.Delete(departmentId);

        }

        public IEnumerable<Department> GetAll()
        {
            return GetAll<Department>();
        }

        public int Insert(Department department)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                // Câu lệnh SQL để thêm một phòng ban mới vào cơ sở dữ liệu
                var sqlCommand = @"
            INSERT INTO Department (DepartmentId, DepartmentCode, DepartmentName, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
            VALUES (@DepartmentId, @DepartmentCode, @DepartmentName, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";

                // Khởi tạo giá trị cho các thuộc tính của department
                department.DepartmentId = Guid.NewGuid();       // Tạo một GUID mới cho DepartmentId
                department.CreatedDate = DateTime.Now;          // Gán thời gian hiện tại cho CreatedDate
                department.ModifiedDate = DateTime.Now;         // Gán thời gian hiện tại cho ModifiedDate
                department.CreatedBy = "lam nguyen hong";       // Tên người tạo
                department.ModifiedBy = "lam nguyen hong";      // Tên người chỉnh sửa

                // Thực hiện câu lệnh SQL với tham số là đối tượng department
                var res = _mySqlConnection.Execute(sqlCommand, param: department);

                // Trả về kết quả (số dòng bị ảnh hưởng)
                return res;
            }

        }

        public int Update(Department department, Guid departmentId)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = @"
    UPDATE Department
    SET DepartmentCode = @DepartmentCode,
        DepartmentName = @DepartmentName,
        ModifiedDate = @ModifiedDate,
        ModifiedBy = @ModifiedBy
    WHERE DepartmentId = @DepartmentId";

                // Khởi tạo giá trị cho các thuộc tính của department
                department.DepartmentId = departmentId;      // Gán giá trị departmentId cho thuộc tính DepartmentId
                department.ModifiedDate = DateTime.Now;      // Gán thời gian hiện tại cho ModifiedDate

                // Thực hiện câu lệnh SQL với tham số là đối tượng department
                var res = _mySqlConnection.Execute(sqlCommand, param: department);

                // Trả về kết quả (số dòng bị ảnh hưởng)
                return res;
            }

        }
    }
}
