using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.Cukcuk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("aaa");
        }

        [HttpGet("EmployeeId")]
        public IActionResult Get(int id)
        {
            return Ok($"Get by id: {id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody]string? ten, int tuoi)
        {
            return Ok($"du lieu vua post la: {ten} ; {tuoi} ");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("api put");
        }
        [HttpDelete("EmployeeId")]
        public IActionResult Delete(int id)
        {
            return Ok($"api delete by id: {id}");
        }

    }
}
