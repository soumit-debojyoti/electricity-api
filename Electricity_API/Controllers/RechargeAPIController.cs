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
            return await this.rs.UpdateRechargeTransactionStatus(status, operatortxnid, joloorderid, userorderid, servicetype);
        }
        [Route("api/transaction/{orderID}/status/{transactionStatus}/message/{errorMessage}")]
        [HttpPost]
        public async Task<bool> UpdateTransactionDetails(string orderID, string transactionStatus, string errorMessage)
        {
            return await this.rs.UpdateTransactionDetails(orderID, transactionStatus, errorMessage);
        }
        [Route("api/transaction/user/{userID}/wallet/amount/{amount}/message/{message}")]
        [HttpPost]
        public async Task<bool> DeductWalletBalance(string userID, string amount, string message)
        {
            return await this.rs.DeductWalletBalance(userID, amount, message);
        }

        [Route("api/validation")]
        [HttpPost]
        public async Task<bool> SaveRechargeAPIValidation([FromBody] RechargeAPI api)
        {
            return await this.rs.InsertValidationApiDetails(api.RechargeMode,api.OperatorName, api.ApiValue);
        }
        [Route("api/validation/rechargemode/{rechargeMode}/operator/{operatorName}")]
        [HttpGet]
        public async Task<RechargeAPI> FetchValidationApiDetails(string rechargeMode, string operatorName)
        {
            return await this.rs.FetchValidationApiDetails(rechargeMode,operatorName);
        }
    }
}