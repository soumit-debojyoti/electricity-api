using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class User_Service
    {
        private Electricity_DAL.User_DAL _user = null;
        private Electricity_DAL.Common_DAL _common = null;
        public User_Service(ConnectionStrings connectionString)
        {
            _user = new Electricity_DAL.User_DAL(connectionString);
            _common = new Electricity_DAL.Common_DAL(connectionString);
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

        public async Task<MobileValidationResponse> ValidateMobileByMobileNumber(string mobile_number)
        {
            return await _user.ValidateMobileByMobileNumber(mobile_number);
        }

        public async Task<MobileUniqueValidationResponse> ValidateUniqueMobileByMobileNumber(string mobile_number)
        {
            return await _user.ValidateUniqueMobileByMobileNumber(mobile_number);
        }

        public async Task<AccountValidationResponse> ValidateAccountByUserId(int userid)
        {
            return await _user.ValidateAccountByUserId(userid);
        }

        public async Task<TodayUserJoinCountResponse> GetTodayUserJoinCount()
        {
            return await _user.GetTodayUserJoinCount();
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

        public async Task<List<TokenDetailsGeneric>> GetUnusedReferalTokenDetails()
        {
            return await _user.GetUnusedReferalTokenDetails();
        }

        public async Task<List<TokenDetailsSpecific>> GetUnusedReferalTokenDetailsByToken(string token)
        {
            return await _user.GetUnusedReferalTokenDetailsByToken(token);
        }

        public async Task<ReferalTokenResponse> GetReferelToken(string userName)
        {
            ReferalTokenResponse response = new ReferalTokenResponse();
            bool isQualify = await _user.QualifyUserToRefer(userName);
            if (isQualify)
            {
                response.token = await _user.GetReferelToken(userName);
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

        public async Task<KYCDetailsResponse> CheckKYCDetail(string userName)
        {
            return await _user.CheckKYCDetail(userName);
        }

        public async Task<List<KYCDetails>> GetkKYCDetail(int user_id)
        {
            return await _user.GetkKYCDetail(user_id);
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

        public async Task<int> InsertKYCInfo(KYCDetails kyc_info)
        {
            return await _user.InsertKYCInfo(kyc_info);
        }

        public async Task<bool> UpdateKYCInfoInUser(int user_id, int kyc_info)
        {
            return await _user.UpdateKYCInfoInUser(user_id, kyc_info);
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

        public async Task<string> ReactivateToken(string token)
        {
            return await _user.ReactivateToken(token);
        }

        public async Task<string> DeactivateToken(string token)
        {
            return await _user.DeactivateToken(token);
        }

        public async Task<string> SurrenderToken(string token)
        {
            return await _user.SurrenderToken(token);
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
        public async Task UpdateNextLevel(string userSecurityStamp)
        {
            var configuration = _common.GetConfiguration().Result;
            await _user.UpdateNextLevel(userSecurityStamp, configuration);

        }

        /// <summary>
        /// Check user count and update rank for introducer
        /// </summary>
        /// <param name="user_security_stamp"></param>
        /// <param name="introducerRank"></param>
        /// <returns></returns>
        //public bool GetUserSamePeer(Introducer introducer)
        //{
        //    return _user.GetUserSamePeer(introducer);
        //}
        /// <summary>
        /// Fetches the User Rank By User ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int FetchUserRank(int userID)
        {
            try
            {
                return _user.FetchUserRank(userID);

            }
            catch
            {

                return 0;
            }

        }

        public async Task<List<WalletTransaction>> FetchAllWalletTransaction(int requestorID, string startDate = null, string endDate = null)
        {
            DateTime sDate, eDate = new DateTime();
            if (startDate == string.Empty || startDate == null || endDate == string.Empty || endDate == null)
            {
                sDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                eDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            else
            {
                sDate = Convert.ToDateTime(startDate);
                eDate = Convert.ToDateTime(endDate);
            }
            return await this._user.FetchAllWalletTransaction(requestorID, sDate, eDate);
        }

        public async Task<List<RankAcheiver>> FetchAllRankAcheiver()
        {
            return await this._user.FetchAllRankAcheiver();
        }

        public async Task<List<IntroducerBonus>> FetchAllReferralBonus()
        {
            return await this._user.FetchAllReferralBonus();
        }

        public async Task<bool> UpdateIntroducerReferralBonus(IntroducerBonus introducerBonus)
        {
            return await this._user.UpdateIntroducerReferralBonus(introducerBonus.IntroducerLevel, introducerBonus.ReferralBonus, introducerBonus.MonthlyPayout);
        }

        public async Task<List<RegisterUser>> FetchAllUserDetails()
        {
            return await this._user.GetAllUsersDetails();
        }

        public async Task<RegisterUser> GetAllUsersDetails(int userID)
        {
            return await this._user.GetAllUsersDetails(userID);
        }
        public async Task<bool> UpdateUserDetails(int userID, RegisterUser user_info)
        {
            return await this._user.UpdateUserDetails(userID, user_info);
        }

        public async Task<CommissionSetting> FetchCommissionAmount(string rechargeType, string operatorName, decimal transactionAmount)
        {
            return await this._user.FetchCommissionAmount(rechargeType, operatorName, transactionAmount);
        }
    }
}
