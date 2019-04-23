using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Electricity_DAL.Models;
using Electricity_Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

                    //var securityKey = us.GetUserKey(usernameAndPass[0]).Result;
                    return Ok(new ResponseModel() { access_token=tokenString,role_id= response.role_id});
                }
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