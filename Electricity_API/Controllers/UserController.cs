using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        Electricity_Service.User_Service rs = null;
        Electricity_Service.Common_Service commonService = null;
        public UserController(IOptions<ConnectionStrings> config)
        {
            this.config = config;
            rs = new User_Service(this.config.Value);
            commonService = new Common_Service(this.config.Value);
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
        public async Task<ActionResult> GetUserByUserName(string username)
        {
            var response = await rs.GetUser(username);
            return Ok(response);
        }

        [Authorize]
        [Route("searchusers/{name}")]
        [HttpGet]
        public async Task<ActionResult> SearchUserByUserName(string name)
        {
            if (name == "all")
            {
                name = "";
            }

            var response = await rs.SearchUser(name);
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
            return Ok(token);
        }

        [Authorize]// GET api/values
        [Route("unusedtokendetails")]
        [HttpGet]
        public async Task<ActionResult> GetUnusedReferalTokenDetails()
        {
            var token = await rs.GetUnusedReferalTokenDetails();
            return Ok(token);
        }

        [Authorize]// GET api/values
        [Route("unusedtokendetails/{token}")]
        [HttpGet]
        public async Task<ActionResult> GetUnusedReferalTokenDetailsByToken(string token)
        {
            var tokens = await rs.GetUnusedReferalTokenDetailsByToken(token);
            return Ok(tokens);
        }

        // GET api/values
        [Route("validatetoken/{token}")]
        [HttpGet]
        public async Task<ActionResult> ValidateReferelToken(string token)
        {
            var isValid = await rs.ValidateReferelToken(token);
            return Ok(isValid);
        }

        // GET api/values
        [Route("walletbalance/user/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetWalletBalance(int userId)
        {
            var walletBalance = await rs.GetWalletBalance(userId);
            return Ok(walletBalance);
        }

        // GET api/values
        [Route("walletbalancereport/user/{userId}/start/{startDate}/end/{endDate}")]
        [HttpGet]
        public async Task<ActionResult> GetWalletTransactionReport(int userId, string startDate, string endDate)
        {
            var walletBalanceReport = await rs.GetWalletTransactionReport(userId, startDate, endDate);
            return Ok(walletBalanceReport);
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

        [Authorize]// GET api/values
        [Route("getkycdetail/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetkKYCDetail(int userId)
        {
            GetKYCDetailsResponse response = new GetKYCDetailsResponse();
            response.list= await rs.GetkKYCDetail(userId);
            return Ok(response);
        }

        [Route("addkyc/{userId}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> AddKYC(int userId)
        {
            bool isSuccess = false;
            if (userId > 0)
            {
                RegisterUserResponse rr = new RegisterUserResponse();
                RegisterUser registerUser = new RegisterUser();
                registerUser.idprooftype = Convert.ToInt32(Request.Form["idprooftype"].ToString());
                registerUser.idproof = Request.Form["idproof"];
                registerUser.addressprooftype = Convert.ToInt32(Request.Form["addressprooftype"].ToString());
                registerUser.addressproof = Request.Form["addressproof"];
                registerUser.bankdetails = Request.Form["bankdetails"];

                KYCDetails kdetail = new KYCDetails();
                kdetail.kyc_detail_id = 0;
                kdetail.id_proof_id = registerUser.idprooftype;
                kdetail.id_proof_document_path = registerUser.idproof;
                kdetail.address_proof_id = registerUser.addressprooftype;
                kdetail.address_proof_document_path = registerUser.addressproof;
                kdetail.bank_details = registerUser.bankdetails;
                int kyc_detail_id = await rs.InsertKYCInfo(kdetail);
                if (kyc_detail_id > 0)
                {
                    isSuccess = await rs.UpdateKYCInfoInUser(userId, kyc_detail_id);
                }
            }
            return Ok(isSuccess);
        }


        [Route("registeruser")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> RegisterUser()//[FromBody] RegisterUser test)
        {
            RegisterUserResponse rr = new RegisterUserResponse();
            RegisterUser registerUser = new RegisterUser();
            registerUser.is_employee = Convert.ToString(Request.Form["isEmployee"]) == "true" ? true : false;
            registerUser.introcode = Request.Form["introcode"];
            registerUser.introname = Request.Form["introname"];
            registerUser.username = Request.Form["username"];
            registerUser.password = Request.Form["password"];
            registerUser.firstName = Request.Form["firstName"];
            registerUser.middleName = Request.Form["middleName"];
            registerUser.lastName = Request.Form["lastName"];
            registerUser.fathername = Request.Form["fathername"];
            registerUser.gender = Convert.ToInt32(Request.Form["gender"].ToString());
            registerUser.dob = Request.Form["dob"];
            registerUser.mobile = Request.Form["mobile"];
            registerUser.email = Request.Form["email"];
            registerUser.pancard = Request.Form["pancard"];
            registerUser.aadharcard = Request.Form["aadharcard"];
            registerUser.address = Request.Form["address"];
            registerUser.po = Request.Form["po"];
            registerUser.ps = Request.Form["ps"];
            registerUser.district = Request.Form["district"];
            registerUser.city = Request.Form["city"];
            registerUser.state = Convert.ToInt32(Request.Form["state"]);
            registerUser.pincode = Request.Form["pincode"];
            registerUser.bankname = Request.Form["bankname"];
            registerUser.accholdername = Request.Form["accholdername"];
            registerUser.accnumber = Request.Form["accnumber"];
            registerUser.ifsc = Request.Form["ifsc"];
            registerUser.branch = Request.Form["branch"];
            registerUser.photo = Request.Form["photo"];
            registerUser.isKYCLater = Convert.ToBoolean(Request.Form["isKYCLater"]);

            if (!registerUser.isKYCLater)
            {
                registerUser.idprooftype = Convert.ToInt32(Request.Form["idprooftype"].ToString());
                registerUser.idproof = Request.Form["idproof"];
                registerUser.addressprooftype = Convert.ToInt32(Request.Form["addressprooftype"].ToString());
                registerUser.addressproof = Request.Form["addressproof"];
                registerUser.bankdetails = Request.Form["bankdetails"];
            }
            


            registerUser.payonline = Convert.ToBoolean(Request.Form["payonline"].ToString() == string.Empty ? false : true);

            BankDetails bdetail = new BankDetails();
            bdetail.bank_detail_id = 0;
            bdetail.bank_name = registerUser.bankname;
            bdetail.account_holder_name = registerUser.accholdername;
            bdetail.account_number = registerUser.accnumber;
            bdetail.ifsc_number = registerUser.ifsc;
            bdetail.branch_name = registerUser.branch;
            bdetail.is_pay_online = registerUser.payonline;
            int bank_detail_id = await rs.InsertBankInfo(bdetail);
            int kyc_detail_id = 0;
            if (!registerUser.isKYCLater)
            {
                KYCDetails kdetail = new KYCDetails();
                kdetail.kyc_detail_id = 0;
                kdetail.id_proof_id = registerUser.idprooftype;
                kdetail.id_proof_document_path = registerUser.idproof;
                kdetail.address_proof_id = registerUser.addressprooftype;
                kdetail.address_proof_document_path = registerUser.addressproof;
                kdetail.bank_details = registerUser.bankdetails;
                kyc_detail_id = await rs.InsertKYCInfo(kdetail);
            }


            UserDetails udetail = new UserDetails();
            udetail.introcode = registerUser.introcode;
            udetail.introname = registerUser.introname;
            udetail.username = registerUser.username;
            udetail.role_id = registerUser.is_employee ? 2 : 3;
            udetail.email = registerUser.email;
            udetail.password = registerUser.password;
            udetail.first_name = registerUser.firstName;
            udetail.last_name = registerUser.lastName;
            udetail.father_name = registerUser.fathername;
            udetail.dob = registerUser.dob;
            udetail.mobile_number = registerUser.mobile;
            udetail.pan_card = registerUser.pancard;
            udetail.aadhar_card = registerUser.aadharcard;
            udetail.address = registerUser.address;
            udetail.post_office = registerUser.po;
            udetail.police_station = registerUser.ps;
            udetail.district = registerUser.district;
            udetail.city = registerUser.city;
            udetail.state = registerUser.state;
            udetail.pin = registerUser.pincode;
            udetail.sex = Enum.GetNames(typeof(Gender)).GetValue(registerUser.gender - 1).ToString();
            udetail.middle_name = registerUser.middleName;
            udetail.bank_detail_id = bank_detail_id;
            udetail.kyc_detail_id = kyc_detail_id;
            udetail.introcode = registerUser.introcode;
            udetail.introname = registerUser.introname;
            udetail.photo = registerUser.photo;
            udetail.is_employee = registerUser.is_employee;
            string user_security_stamp = await rs.InsertUserInfo(udetail);
            rr.message = "Registered.";
            rr.user_security_stamp = user_security_stamp;
            RegisterTokenResponse rtr = new RegisterTokenResponse();
            rtr.message = await rs.RegisterToken(registerUser.introcode, user_security_stamp);

            //Soumit// After this call a procedure to calculate the joining bonus and before the call a sp to get the below outputs from a user_security_stamp
            //Immediate parent model
            //List of shiblings
            //List of user under this user.

            // ------- Added for Rank Achievement
            #region Rank Achievement
            if (rs.UpdateUserRank(user_security_stamp, 0))
            {
                await rs.UpdateNextLevel(user_security_stamp);

            }
            #endregion

            /* Registration successfull: registered user should recieve email for successful registration*/
            string emailBody = "Hi, <br />" + registerUser.firstName + " " + registerUser.lastName + " you have been successfully registered to Telecharge.<br />" +
                                "user name - " + registerUser.username + "<br />password - " + registerUser.password + "<br />Thanks and Regards,<br>Telecharge Registration Team" +
                                "<br />This is a system generated email, do not reply back to this email-id";
            //string emailBody = "Hi, <br />Your Login ID/ User Name - " + registerUser.username + "<br/>Password - " + registerUser.password;
            string emailSubject = "Telecharge - Registration";
            string fromEmailID = "postmaster@telecharge.biz";
            await commonService.SendEmail(registerUser.email, emailSubject, emailBody, fromEmailID);
            //await commonService.IsWithdrawalRequestSendByThisUser(5);
            return Ok(rr);
        }

        [Route("registertoken")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> RegisterToken(string security_number, string security_stamp_of_new_user)
        {
            RegisterTokenResponse rtr = new RegisterTokenResponse();
            rtr.message = await rs.RegisterToken(security_number, security_stamp_of_new_user);
            return Ok(rtr);
        }

        [Authorize]
        [Route("reactivatetoken/{token}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> ReactivateToken(string token)
        {
            ReactivateTokenResponse rtr = new ReactivateTokenResponse();
            rtr.message = await rs.ReactivateToken(token);
            return Ok(rtr);
        }

        [Authorize]
        [Route("deactivatetoken/{token}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> DeactivateToken(string token)
        {
            DeactivateTokenResponse rtr = new DeactivateTokenResponse();
            rtr.message = await rs.DeactivateToken(token);
            return Ok(rtr);
        }

        [Authorize]
        [Route("surrendertoken/{token}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> SurrenderToken(string token)
        {
            SurrenderTokenResponse rtr = new SurrenderTokenResponse();
            rtr.message = await rs.SurrenderToken(token);
            return Ok(rtr);
        }

        [Route("rank/user/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetRankAchieverList(int userId)
        {
            RankAchieverModel rtr = new RankAchieverModel();
            rtr = await rs.GetRankAchieverList(userId);
            return Ok(rtr);
        }

        [Route("rank/user/count/{userId}")]
        [HttpGet]
        public async Task<ActionResult> GetRankAchieverListCount(int userId)
        {
            RankAchieverCountModel rtr = new RankAchieverCountModel();
            rtr = await rs.GetRankAchieverListCount(userId);
            return Ok(rtr);
        }

        [Route("exist/{userName}")]
        [HttpGet]
        public async Task<ActionResult> FindUser(string userName)
        {
            bool rtr = await rs.FindUser(userName);
            return Ok(rtr);
        }

        [Authorize]
        [Route("balancetransfer/sender/{senderId}/receiver/{receiverId}/amount/{amount}/comment/{comment}")]
        [HttpPost]
        public async Task<ActionResult> BalanceTransfer(int senderId, int receiverId, decimal amount, string comment)
        {
            TransferAmountModel response = await rs.BalanceTransfer(senderId, receiverId, amount, comment);
            return Ok(response);
        }

        [HttpGet("{userID}/userrank")]
        public int FetchUserRank(int userID)
        {
            return rs.FetchUserRank(userID);
        }

        [HttpGet("{requestorID}/AllTransaction/from/{startDate}/to/{endDate}")]
        public async Task<List<WalletTransaction>> FetchAllTransaction(int requestorID,string startDate, string endDate)
        {
            return await rs.FetchAllWalletTransaction(requestorID, startDate, endDate);
        }
    }
}