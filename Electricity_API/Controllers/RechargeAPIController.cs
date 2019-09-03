using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Electricity_DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Electricity_DAL.Models;

namespace Electricity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargeAPIController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.RechargeApi rs = null;
        private IHostingEnvironment _hostingEnvironment;
        public RechargeAPIController(IOptions<ConnectionStrings> config, IHostingEnvironment hostingEnvironment)
        {
            this.config = config;
            rs = new Electricity_Service.RechargeApi(this.config.Value);
            _hostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        [Route("api")]
        [HttpPost]
        public async Task<bool> SaveRechargeAPI([FromBody] RechargeAPI api)
        {
            return await this.rs.SaveRechargeAPI(api.RechargeMode, api.OperatorName, api.ApiValue);
        }

        [Route("api/{rechargeMode}")]
        [HttpGet]
        public async Task<List<RechargeAPI>> GetRechargeAPI(string rechargeMode)
        {
            return await this.rs.GetRechargeAPI(rechargeMode);
        }

        [Route("api/recharge/{userID}/rechargemode/{rechargeMode}/amount/{rechargeAmount}")]
        [HttpPost]
        public async Task<int> InsertTransaction(string userID, string rechargeMode, string rechargeAmount)
        {
            return await this.rs.InsertTransaction(userID, rechargeMode, rechargeAmount);
        }
        //[Route("api/response/status/{status}/transactionid/{operatortxnid}/joloorderid/{joloorderid}/userorderid/{userorderid}/servicetype/{servicetype}")]
        [Route("api/response")]
        [HttpPost]
        public async Task<bool> GetJoloAPIResponse(string status, string operatortxnid, string joloorderid, string userorderid, int servicetype)
        {
            return true;
        }
    }
}