using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Infrastructure.Repository;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : MISABaseController<Positions>
    {
        IPositionsRepository _positionsRepository;
        IPositionsService _positionsService;

        public PositionsController(IPositionsRepository positionsRepository, IPositionsService positionsService):base(positionsRepository, positionsService)
        {
            _positionsRepository = positionsRepository;
            _positionsService = positionsService;
        }
        
    }

}
