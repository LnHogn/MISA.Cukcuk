using MISA.Web.Core.Entities;
using MISA.Web.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Infrastructure.Repository
{
    public class PositionsRepository : BaseRepository<Positions>, IPositionsRepository
    {
        public IEnumerable<Positions> GetAll()
        {
            return GetAll<Positions>();
        }
    }
}
