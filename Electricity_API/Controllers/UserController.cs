using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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
        public async Task<ActionResult> GetUserByUserName(string username)
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
            return Ok(token);
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
            registerUser.idprooftype = Convert.ToInt32(Request.Form["idprooftype"].ToString());
            registerUser.idproof = Request.Form["idproof"];
            registerUser.addressprooftype = Convert.ToInt32(Request.Form["addressprooftype"].ToString());
            registerUser.addressproof = Request.Form["addressproof"];
            registerUser.photo = Request.Form["photo"];
            registerUser.bankdetails = Request.Form["bankdetails"];
            registerUser.payonline = Convert.ToBoolean(Request.Form["payonline"].ToString() == string.Empty ? false : true);

            BankDetails bdetail = new BankDetails();
            bdetail.bank_detail_id = 0;
            bdetail.bank_name = registerUser.bankname;
            bdetail.account_holder_name = registerUser.accholdername;
            bdetail.account_number = registerUser.accnumber;
            bdetail.ifsc_number = registerUser.ifsc;
            bdetail.branch_name = registerUser.branch;
            bdetail.id_proof_id = registerUser.idprooftype;
            bdetail.id_proof_document_path = registerUser.idproof;
            bdetail.photo = registerUser.photo;
            bdetail.address_proof_id = registerUser.addressprooftype;
            bdetail.address_proof_document_path = registerUser.addressproof;
            bdetail.bank_details = registerUser.bankdetails;
            bdetail.is_pay_online = registerUser.payonline;

            int bank_detail_id = await rs.InsertBankInfo(bdetail);



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
            udetail.introcode = registerUser.introcode;
            udetail.introname = registerUser.introname;
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
    }
}