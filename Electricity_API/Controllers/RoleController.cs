using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.Role_Service rs = null;
        public RoleController(IOptions<ConnectionStrings> config)
        {
            this.config = config;
            rs = new Role_Service(this.config.Value);
        }

        // GET api/values
        [Route("roles")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await rs.GetRoles();
            return Ok(list);
        }
    }
}