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
    public class PositionsRepository : BaseRepository<Positions>, IPositionsRepository
    {
        public int Delete(Guid positionsId)
        {
            return base.Delete(positionsId);
        }

        public IEnumerable<Positions> GetAll()
        {
            return GetAll<Positions>();
        }

        public int Insert(Positions positions)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                // Câu lệnh SQL để thêm một vị trí mới vào cơ sở dữ liệu
                var sqlCommand = @"
        INSERT INTO Positions (PositionId, PositionCode, PositionName, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
        VALUES (@PositionId, @PositionCode, @PositionName, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy)";

                // Khởi tạo giá trị cho các thuộc tính của position
                positions.PositionId = Guid.NewGuid();       // Tạo một GUID mới cho PositionId
                positions.CreatedDate = DateTime.Now;        // Gán thời gian hiện tại cho CreatedDate
                positions.ModifiedDate = DateTime.Now;       // Gán thời gian hiện tại cho ModifiedDate
                positions.CreatedBy = "lam nguyen hong";     // Tên người tạo
                positions.ModifiedBy = "lam nguyen hong";    // Tên người chỉnh sửa

                // Thực hiện câu lệnh SQL với tham số là đối tượng position
                var res = _mySqlConnection.Execute(sqlCommand, param: positions);

                // Trả về kết quả (số dòng bị ảnh hưởng)
                return res;
            }
        }

        public int Update(Positions positions, Guid positionId)
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = @"
        UPDATE Positions
        SET PositionCode = @PositionCode,
            PositionName = @PositionName,
            ModifiedDate = @ModifiedDate,
            ModifiedBy = @ModifiedBy
        WHERE PositionId = @PositionId";

                // Khởi tạo giá trị cho các thuộc tính của position
                positions.PositionId = positionId;          // Gán giá trị positionId cho thuộc tính PositionId
                positions.ModifiedDate = DateTime.Now;      // Gán thời gian hiện tại cho ModifiedDate

                // Thực hiện câu lệnh SQL với tham số là đối tượng position
                var res = _mySqlConnection.Execute(sqlCommand, param: positions);

                // Trả về kết quả (số dòng bị ảnh hưởng)
                return res;
            }
        }
    }
}
