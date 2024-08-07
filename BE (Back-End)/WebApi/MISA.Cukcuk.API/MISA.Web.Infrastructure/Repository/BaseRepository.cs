using Dapper;
using MISA.Web.Core.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Infrastructure.Repository
{
    public class BaseRepository<MISAEntity>
    {
        protected readonly string _connectionString = "Host=8.222.228.150; Port=3306; Database=HAUI_2021600453_LamNguyenHong; User id=manhnv; Password=12345678";
        protected MySqlConnection _mySqlConnection;

        /// <summary>
        /// lay toan bo du lieu
        /// </summary>
        /// <typeparam name="MISAEntity">type object</typeparam>
        /// <returns>danh sach object</returns>
        /// created by lam nguyen hong(7/8/2024)
        public IEnumerable<MISAEntity> GetAll<MISAEntity>()
        {
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var className = typeof(MISAEntity).Name;
                //lay du lieu
                var sqlCommand = $"select * from {className}";
                var entities = _mySqlConnection.Query<MISAEntity>(sql: sqlCommand);
                //tra ket qua
                return entities;
            }
        }

        public MISAEntity GetById(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;

            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                //lay du lieu
                var sqlCommand = $"select * from {className} where {className}Id = @{className}Id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{className}d", entityId);
                //truy van du lieu trong database
                var entity = _mySqlConnection.QueryFirstOrDefault<MISAEntity>(sql: sqlCommand, param: dynamicParameters);
                //tra ket qua
                return entity;
            }
        }

    }
}
