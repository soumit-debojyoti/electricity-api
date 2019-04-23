using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class User_Service
    {
        private Electricity_DAL.User_DAL _user = null;
        public User_Service(ConnectionStrings connectionString)
        {
            _user = new Electricity_DAL.User_DAL(connectionString);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _user.Get_Users();
        }

        public async Task<User> GetUser(string user_name)
        {
            return await _user.Get_User(user_name);
        }

        public async Task<string> GetUserKey(string userName)
        {
            List<User> users = await _user.Get_Users();
            var securityKey = users.Find(x => x.user_name == userName).security_stamp;
            return securityKey;
        }

        public async Task<FindUserResponse> FindUser(string userName, string password)
        {
            return await _user.Find_Users(userName, password);
        }

        public async Task<bool> QualifyUserToRefer(string userName)
        {
            return await _user.QualifyUserToRefer(userName);
        }

        public async Task<ReferalTokenResponse> GetReferelToken(string userName)
        {
            ReferalTokenResponse response = new ReferalTokenResponse();
            bool isQualify = await _user.QualifyUserToRefer(userName);
            if (isQualify)
            {
                response.token= await _user.GetReferelToken(userName);
                return response;
            }
            else
            {
                response.token = string.Empty;
                return response;
            }
        }

        public async Task<ValidateReferalTokenResponse> ValidateReferelToken(string token)
        {
            return await _user.ValidateReferelToken(token);
        }

        public async Task<bool> ValidateUserToRefer(string userName)
        {
            return await _user.ValidateUserToRefer(userName);
        }

        public async Task<ReferalResponseModel> ReferUserWithToken(string userName, string token)
        {
            bool isQualify = await _user.ValidateUserToRefer(userName);
            if (isQualify)
            {
                ValidateReferalTokenResponse validateReferalTokenResponse = await _user.ValidateReferelToken(token);
                if (validateReferalTokenResponse.is_valid)
                {
                    bool isSucess = await _user.ReferUserWithToken(userName, token);
                    if (isSucess)
                    {
                        return new ReferalResponseModel() { IsSuccess = isSucess, Reason = "" };// false;
                    }
                    else
                    {
                        return new ReferalResponseModel() { IsSuccess = isSucess, Reason = "Token is already used for another referal." };// false;
                    }

                }
                else
                {
                    return new ReferalResponseModel() { IsSuccess = false, Reason = "Token is been used by someone or may be validity is expired." };// false;
                }
            }
            else
            {
                return new ReferalResponseModel() { IsSuccess = false, Reason = "User not qualify for referal" };// false;
            }

        }

        public async Task<int> InsertBankInfo(BankDetails bank_info)
        {
            return await _user.InsertBankInfo(bank_info);
        }

        public async Task<int> InsertUserInfo(UserDetails user_info)
        {
            return await _user.InsertUserInfo(user_info);
        }
    }
}
