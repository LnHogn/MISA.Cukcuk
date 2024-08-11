using MISA.Web.Core.Entities;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web.Core.Services
{
    public class PositionsService :BaseService<Positions>, IPositionsService
    {
        IPositionsRepository _positionsRepository;
        public PositionsService(IPositionsRepository positionsRepository):base(positionsRepository)
        {
            _positionsRepository = positionsRepository;
        }
        
    }
}
