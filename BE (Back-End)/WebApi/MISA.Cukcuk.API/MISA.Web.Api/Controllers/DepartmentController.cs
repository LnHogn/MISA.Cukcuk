using Microsoft.AspNetCore.Mvc;
using MISA.Web.Core.Interfaces.Infrastructure;
using MISA.Web.Core.Entities;
using MISA.Web.Core.Exceptions;
using MISA.Web.Infrastructure.Repository;
using MISA.Web.Core.Interfaces.Services;
using MISA.Web.Core.Services;

namespace MISA.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : MISABaseController<Department>
    {

        IDepartmentRepository _departmentRepository;
        IDepartmentService _departmentService;
        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentService departmentService):base(departmentRepository, departmentService)
        {
            _departmentRepository = departmentRepository;
            _departmentService = departmentService;
        }
       
    
    }
}
