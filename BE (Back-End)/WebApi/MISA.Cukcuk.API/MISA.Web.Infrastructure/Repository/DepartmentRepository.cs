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
            return base.Insert(department);

        }

        public int Update(Department department, Guid departmentId)
        {
            return base.Update(department, departmentId);
        }

       
    }
}
