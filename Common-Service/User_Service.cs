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

        public async Task<List<User>> SearchUser(string user_name)
        {
            return await _user.SearchUser(user_name);
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

        public async Task<UserWalletBalanceResponse> GetWalletBalance(int userId)
        {
            return await _user.GetWalletBalance(userId);
        }

        public async Task<WalletReportResponse> GetWalletTransactionReport(int userId, string start_date, string end_date)
        {
            return await _user.GetWalletTransactionReport(userId, start_date, end_date);
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

        public async Task<string> InsertUserInfo(UserDetails user_info)
        {
            return await _user.InsertUserInfo(user_info);
        }

        public async Task<string> RegisterToken(string security_number, string security_stamp_of_new_user)
        {
            return await _user.RegisterToken(security_number, security_stamp_of_new_user);
        }

        public async Task<RankAchieverModel> GetRankAchieverList(int user_id)
        {
            return await _user.GetRankAchieverList(user_id);
        }

        public async Task<RankAchieverCountModel> GetRankAchieverListCount(int user_id)
        {
            return await _user.GetRankAchieverListCount(user_id);
        }

        public async Task<TransferAmountModel> BalanceTransfer(int senderId, int receiverId, decimal amount, string comment)
        {
            return await _user.BalanceTransfer(senderId, receiverId, amount, comment);
        }

        public async Task<bool> FindUser(string user_name)
        {
            return await _user.FindUser(user_name);
        }

        public bool UpdateUserRank(string userSecurityStamp, int userRank)
        {
            return _user.UpdateUserRank(userSecurityStamp, userRank) > 0;
        }

        //public List<RankUser> GetUserSamePeer(string securityStamp)
        //{
        //    return _user.GetUserSamePeer(securityStamp);
        //}
        /// <summary>
        /// Fetches the Introducer Information
        /// </summary>
        /// <param name="securityStamp"></param>
        /// <returns></returns>
        public Introducer GetIntroducerInfo(string securityStamp)
        {
            return _user.GetIntroducerInfo(securityStamp);
        }
        /// <summary>
        /// Fetch user Rank
        /// </summary>
        /// <param name="userSecurityStamp"></param>
        /// <returns></returns>
        public RankUser FetchUserRank(string userSecurityStamp)
        {
            return _user.FetchUserRank(userSecurityStamp);
        }
        /// <summary>
        /// Updates the rank to next level
        /// </summary>
        /// <param name="userSecurityStamp"></param>
        /// <param name="userRank"></param>
        /// <returns></returns>
        public void UpdateNextLevel(string userSecurityStamp)
        {
            bool response = true;
            while (response)
            {
                response = false;
                Introducer introducer = _user.GetIntroducerInfo(userSecurityStamp);
                if (introducer != null && introducer.RoleID != 4)
                {
                    response = GetUserSamePeer(introducer);
                    userSecurityStamp = introducer.SecurityStamp;
                }
            }

        }

        /// <summary>
        /// Check user count and update rank for introducer
        /// </summary>
        /// <param name="user_security_stamp"></param>
        /// <param name="introducerRank"></param>
        /// <returns></returns>
        public bool GetUserSamePeer(Introducer introducer)
        {
            bool isIntroducerRankUpdated = false;
            //Introducer introducer = _user.GetIntroducerInfo(user_security_stamp);
            if (introducer != null && introducer.RoleID != 4)
            {
                var introducerRank = 0;
                //rs.UpdateUserRank(introducer.SecurityStamp, 0);
                if (_user.FetchUserRank(introducer.SecurityStamp) != null)
                {
                    introducerRank = _user.FetchUserRank(introducer.SecurityStamp).UserRank;
                }
                else
                {
                    _user.UpdateUserRank(introducer.SecurityStamp, 0);

                }
                // 1st Level
                if (introducerRank == 0)
                {
                    if (_user.GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= 2 && introducer.JoiningDate.AddDays(-70) <= DateTime.Now)
                    {
                        _user.UpdateUserRank(introducer.SecurityStamp, 1);
                        isIntroducerRankUpdated = true;
                    }
                    else
                    {
                        _user.UpdateUserRank(introducer.SecurityStamp, 0);
                    }
                }
                // 2nd Level
                else if (introducerRank == 1 && introducer.JoiningDate.AddDays(-120) <= DateTime.Now)
                {
                    //
                    if (_user.GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= 2)
                    {
                        _user.UpdateUserRank(introducer.SecurityStamp, 2);
                        isIntroducerRankUpdated = true;
                    }
                }
            }
            return isIntroducerRankUpdated; 
        }
        /// <summary>
        /// Fetches the User Rank By User ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int FetchUserRank(int userID)
        {
            try
            {
                return _user.FetchUserRank(
               _user.FetchUserSecurityStamp(userID)).UserRank;
            }
            catch
            {

                return 0;
            }
           
        }
    }
}
