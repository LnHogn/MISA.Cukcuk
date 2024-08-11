using Dapper;
using MISA.Web.Core.Entities;
using MISA.Web.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
                dynamicParameters.Add($"@{className}Id", entityId);
                //truy van du lieu trong database
                var entity = _mySqlConnection.QueryFirstOrDefault<MISAEntity>(sql: sqlCommand, param: dynamicParameters);
                //tra ket qua
                return entity;
            }
        }

        public int Delete(Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = @Id";
                var res = _mySqlConnection.Execute(sqlCommand, new { Id = entityId });
                return res;
            }

        }

        public virtual int Insert(MISAEntity entity)
        {
            var className = typeof(MISAEntity).Name;
            var sqlColumsName = new StringBuilder();
            var sqlColumsValue = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            //duyet tat ca prop
            var props = typeof(MISAEntity).GetProperties();
            string delimiter = "";
            foreach (var prop in props)
            {
                //lay ten prop
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                //kiem tra prop co la primarykey
                var primaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                if (primaryKey == true || propName == $"{className}Id")
                {
                    if(prop.PropertyType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }
                }
                var addDate = Attribute.IsDefined(prop, typeof(DateAddorMod));
                if(addDate == true)
                {
                    propValue = DateTime.Now;
                }

                var addby = Attribute.IsDefined(prop, typeof(AddorModBy));
                if (addby == true)
                {
                    propValue = "lam nguyen hong";
                }

                var paramName = $"@{propName}";
                sqlColumsName.Append($"{delimiter}{propName}");
                sqlColumsValue.Append($"{delimiter}{paramName}");
                delimiter = ",";
                parameters.Add(paramName, propValue );

            }
            var sqlCommand = $"INSERT INTO {className}({sqlColumsName.ToString()}) VALUE ({sqlColumsValue.ToString()})";
            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var res = _mySqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        public virtual int Update(MISAEntity entity, Guid entityId)
        {
            var className = typeof(MISAEntity).Name;
            var sqlSetClause = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();

            // Duyệt tất cả các thuộc tính (properties)
            var props = typeof(MISAEntity).GetProperties();
            string delimiter = "";

            foreach (var prop in props)
            {
                // Lấy tên thuộc tính
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                // Kiểm tra thuộc tính có phải là PrimaryKey hay không
                var primaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                if (primaryKey || propName == $"{className}Id")
                {
                    // PrimaryKey sẽ được sử dụng trong mệnh đề WHERE, không cần cập nhật
                    parameters.Add($"@{propName}", entityId);
                    continue;
                }

                // Nếu thuộc tính có thuộc tính `DateAddorMod`, cập nhật với thời gian hiện tại
                var addDate = Attribute.IsDefined(prop, typeof(DateAddorMod));
                if (addDate)
                {
                    propValue = DateTime.Now;
                }

                // Nếu thuộc tính có thuộc tính `AddorModBy`, cập nhật với tên người dùng
                var addby = Attribute.IsDefined(prop, typeof(AddorModBy));
                if (addby)
                {
                    propValue = "lam nguyen hong";
                }

                // Xây dựng mệnh đề SET cho câu lệnh UPDATE
                sqlSetClause.Append($"{delimiter}{propName} = @{propName}");
                delimiter = ",";
                parameters.Add($"@{propName}", propValue);
            }

            // Xây dựng câu lệnh SQL UPDATE
            var sqlCommand = $"UPDATE {className} SET {sqlSetClause.ToString()} WHERE {className}Id = @{className}Id";

            using (_mySqlConnection = new MySqlConnection(_connectionString))
            {
                var res = _mySqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }



    }
}
