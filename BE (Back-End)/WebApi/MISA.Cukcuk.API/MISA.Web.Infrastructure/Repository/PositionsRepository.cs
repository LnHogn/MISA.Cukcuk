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
            return base.Insert(positions);
        }

        public int Update(Positions positions, Guid positionId)
        {
            return base.Update(positions, positionId);
        }
    }
}
