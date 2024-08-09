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
        public IEnumerable<Department> GetAll()
        {
            return GetAll<Department>();
        }

        
    }
}
