using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.User_Service rs = null;
        public UserController(IOptions<ConnectionStrings> config)
        {
            this.config = config;
            rs = new User_Service(this.config.Value);
        }

        // GET api/values
        [Authorize]
        [Route("users")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await rs.GetUsers();
            return Ok(list);
        }

        // GET api/values
        [Authorize]
        [Route("users/{username}")]
        [HttpGet]
        public async Task<ActionResult> GetUserByUserName(string username,string text,string text1)
        {
            var response = await rs.GetUser(username);
            return Ok(response);
        }

        [Authorize]// GET api/values
        [Route("qualifyusertorefer/{userId}")]
        [HttpGet]
        public async Task<ActionResult> QualifyUserToRefer(string userId)
        {
            var isQualified = await rs.QualifyUserToRefer(userId);
            return Ok(isQualified);
        }

        [Authorize]// GET api/values
        [Route("referaltoken/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetReferalToken(string userId)
        {
            var token = await rs.GetReferelToken(userId);
            if (string.IsNullOrEmpty(token))
            {
                return Ok("User is not qualify to refer.");  //(HttpStatusCode.Forbidden, "RFID is disabled for this site.");//Forbid();
            }
            else
            {
                return Ok(token);
            }

        }

        // GET api/values
        [Route("validatetoken/{token}")]
        [HttpGet]
        public async Task<ActionResult> ValidateReferelToken(string token)
        {
            var isValid = await rs.ValidateReferelToken(token);
            return Ok(isValid);
        }



       

        [Authorize]// GET api/values
        [Route("validateusertorefer/{userId}")]
        [HttpGet]
        public async Task<ActionResult> ValidateUserToRefer(string userId)
        {
            var isValid = await rs.ValidateUserToRefer(userId);
            return Ok(isValid);
        }

        [Authorize]// GET api/values
        [Route("referuser/user/{userId}/token/{token}")]
        [HttpGet]
        public async Task<ActionResult> ReferUserWithToken(string userId, string token)
        {
            ReferalResponseModel response = await rs.ReferUserWithToken(userId, token);
            if (response.IsSuccess)
            {
                return Ok(response.IsSuccess);
            }
            else
            {
                return Ok(response.Reason);
            }
        }

        [Route("registeruser")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult RegisterUser([FromBody] RegisterUser test)
        {
            var tt = Request.Body;
            return Ok("Registered.");

        }

    }
}