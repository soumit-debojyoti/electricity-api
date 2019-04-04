using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.Employee_Service es = null;
        public EmployeeController(IOptions<ConnectionStrings> config)
        {
            this.config = config;
            es = new Employee_Service(this.config.Value);
        }

        // GET api/values
        [Route("employees")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await es.GetEmployees();
            return Ok(list);
        }
    }
}