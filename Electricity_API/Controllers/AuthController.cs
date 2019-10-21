using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<ConnectionStrings> config;
        User_Service us = null;
        public AuthController(IOptions<ConnectionStrings> config)
        {
            this.config = config;
            us = new User_Service(this.config.Value);
        }

        //[EnableCors("MyPolicy")]
        [HttpPost("token")]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Token()
        {
            //string tokenString = "test";
            try
            {
                var header = Request.Headers["Authorization"];
                if (header.ToString().StartsWith("Basic"))
                {
                    var credValue = header.ToString().Substring("Basic ".Length).Trim();
                    var usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)); //admin:pass
                    var usernameAndPass = usernameAndPassenc.Split(":");
                    //check in DB username and pass exist
                    FindUserResponse response = await IsUserExist(usernameAndPass[0], usernameAndPass[1]);
                    if (response.IsUserExist)
                    {

                        var claimsdata = new[] { new Claim(ClaimTypes.Name, usernameAndPass[0]) };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ahbasshfbsahjfbshajbfhjasbfashjbfsajhfvashjfashfbsahfbsahfksdjf"));
                        var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                        var token = new JwtSecurityToken(
                             issuer: "mysite.com",
                             audience: "mysite.com",
                             expires: DateTime.Now.AddMinutes(1),
                             claims: claimsdata,
                             signingCredentials: signInCred
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                        KYCDetailsResponse responseKYC = new KYCDetailsResponse();
                        responseKYC = await us.CheckKYCDetail(usernameAndPass[0]);
                        response.message = responseKYC.message;
                        if (responseKYC.is_success)
                        {
                            return Ok(new ResponseModel() { isLoginSuccess = true, access_token = tokenString, role_id = response.role_id, user_id = response.user_id, message = response.message });
                        }
                        else
                        {
                            return Ok(new ResponseModel() { isLoginSuccess = false, access_token = tokenString, role_id = response.role_id, user_id = response.user_id, message = response.message });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return Forbid("Username and Password not matching..");

            // return View();
        }

        private async Task<FindUserResponse> IsUserExist(string user_name, string password)
        {
            return await us.FindUser(user_name, password);

        }
    }
}