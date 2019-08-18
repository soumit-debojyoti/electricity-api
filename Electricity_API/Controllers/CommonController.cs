using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.Common_Service rs = null;
        private IHostingEnvironment _hostingEnvironment;
        public CommonController(IOptions<ConnectionStrings> config, IHostingEnvironment hostingEnvironment)
        {
            this.config = config;
            rs = new Common_Service(this.config.Value);
            _hostingEnvironment = hostingEnvironment;
        }


        // GET api/values
        [Route("addressproofs")]
        [HttpGet]
        public async Task<ActionResult> GetAddressProof()
        {
            List<AddressProof> addressProof = await rs.GetAddressProof();
            return Ok(addressProof);
        }

        // GET api/values
        [Route("idproofs")]
        [HttpGet]
        public async Task<ActionResult> GetIdProof()
        {
            List<IdProof> addressProof = await rs.GetIdProof();
            return Ok(addressProof);
        }

        // GET api/values
        [Route("states")]
        [HttpGet]
        public async Task<ActionResult> GetStates()
        {
            List<State> states = await rs.GetStates();
            return Ok(states);
        }

        [Route("upload/{filetype}")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile(string filetype)
        {

            string fullPath = string.Empty;
            string fileName = string.Empty;
            try
            {
                var file = Request.Form.Files[0];
                string folderName = filetype;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(filetype + "/" + fileName);
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: ");
            }
        }

        [Route("uploadphoto/{fileType}/{fileName}")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadPhoto(string fileType, string fileName)
        {

            string fullPath = string.Empty;
            string filename = string.Empty;
            try
            {
                var file = Request.Form.Files[0];
                string folderName = fileType;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = fileName + "." + filename.Split(".")[1];
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Ok(fileType + "/" + fileName);
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: ");
            }
        }


        [Route("reuploadphoto/{userName}/{fileType}/{fileName}/{oldfileName}/{oldfileExtension}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> ReUploadPhoto(string userName, string fileType, string fileName, string oldfileName, string oldfileExtension)
        {

            string fullPath = string.Empty;
            string filename = string.Empty;
            bool isSuccess = false;
            string oldImage = string.Empty;
            try
            {
               
                var file = Request.Form.Files[0];
                string folderName = fileType;
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fileName = fileName + "." + filename.Split(".")[1];
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    System.IO.File.Delete(Path.Combine(newPath, oldfileName+"."+ oldfileExtension));
                }
                fullPath = fileType + "/" + fileName;
                isSuccess = await rs.ReUploadPhoto(userName, fullPath);
                return Ok(fullPath);
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: ");
            }
        }

        [Route("pagepermission/role/{roleId}")]
        [HttpGet]
        public async Task<ActionResult> PagePermission(string roleId)
        {
            PagePermissionResponse pages = await rs.PagePermission(roleId);
            return Ok(pages);
        }

        [Route("configuration")]
        [HttpGet]
        public async Task<ActionResult> GetConfiguration()
        {
            ConfigurationModel pages = await rs.GetConfiguration();
            return Ok(pages);
        }

        [Route("configuration")]
        [HttpPut]
        public async Task<ActionResult> UpdateConfiguration(ConfigurationModel config)
        {
            string pages = await rs.UpdateConfiguration(config);
            return Ok(pages);
        }

        [Route("wallet/{user_security_stamp}")]
        [HttpPost]
        public async Task<ActionResult> AddWallet(string user_security_stamp)
        {
            AddWalletResponse response = await rs.AddWallet(user_security_stamp);
            return Ok(response);
        }

        [Route("wallettransaction")]
        [HttpPost]
        public async Task<ActionResult> AddWalletTransaction()
        {
            double amount_wallet = Convert.ToDouble(Request.Form["amount"]);
            int userId = Convert.ToInt32(Request.Form["userId"]);
            string message = Convert.ToString(Request.Form["message"]);
            string transaction_mode = Convert.ToString(Request.Form["transactionMode"]);
            AddWalletTrnsactionResponse response = await rs.AddWalletTransaction(amount_wallet, userId, message, transaction_mode);
            return Ok(response);
        }

        [Authorize]
        [Route("walletwidthdrawal/user/{requestInitiatorId}/comment/{comment}")]
        [HttpPost]
        public async Task<ActionResult> AddWalletWidthdrawalRequest(int requestInitiatorId,string comment)
        {
            WalletWidthdrawalResponse response = await rs.AddWalletWidthdrawalRequest(requestInitiatorId, comment);
            return Ok(response);
        }

        [Authorize]
        [Route("requestbalance/user/{requestInitiatorId}/amount/{amount}/comment/{comment}")]
        [HttpPost]
        public async Task<ActionResult> AddWalletBalanceRequest(int requestInitiatorId,decimal amount, string comment)
        {
            BalanceRequestResponse response = await rs.AddWalletBalanceRequest(requestInitiatorId, amount, comment);
            return Ok(response);
        }

        [Authorize]
        [Route("requestbalancededuct/user/{requestInitiatorId}/amount/{amount}/comment/{comment}")]
        [HttpPost]
        public async Task<ActionResult> DeductWalletBalanceRequest(int requestInitiatorId, decimal amount, string comment)
        {
            BalanceRequestResponse response = await rs.DeductWalletBalanceRequest(requestInitiatorId, amount, comment);
            return Ok(response);
        }

        [Authorize]
        [Route("adminwalletwithdrawalapprovalnotification/user/{userId}")]
        [HttpGet]
        public async Task<ActionResult> AdminApprovalNotification(int userId)
        {
            AdminApprovalNotificationModel response = await rs.AdminApprovalNotification(userId);
            return Ok(response);
        }

        [Authorize]
        [Route("adminadddeductwalletapprovalnotification/user/{userId}")]
        [HttpGet]
        public async Task<ActionResult> AdminAddDeductWalletApprovalNotification(int userId)
        {
            AdminWalletAddDeductApprovalNotificationModel response = await rs.AdminAddDeductWalletApprovalNotification(userId);
            return Ok(response);
        }

        //[Authorize]
        [Route("walletwidthdrawal/approve")]
        [HttpPost]
        public async Task<ActionResult> UpdateWalletWithdrawal([FromBody] List<WithdrawalWallet> withdrawalWalletModels)
        {
            List<WithdrawalWallet> response = await rs.UpdateWalletWithdrawal(withdrawalWalletModels);
           return Ok(response);
        }

        //[Authorize]
        [Route("walletadddeduct/approve")]
        [HttpPost]
        public async Task<ActionResult> UpdateWalletAddDeduct([FromBody] List<AddDeductWallet> withdrawalWalletModels)
        {
            List<AddDeductWallet> response = await rs.UpdateWalletAddDeduct(withdrawalWalletModels);
            return Ok(response);
        }

        [Authorize]
        [Route("withdrawalrequestfinder/user/{userId}")]
        [HttpGet]
        public async Task<ActionResult> IsWithdrawalRequestSendByThisUser(int userId)
        {
            bool response = await rs.IsWithdrawalRequestSendByThisUser(userId);
            return Ok(response);
        }
    }
}