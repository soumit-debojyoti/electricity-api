using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_Service
{
    public class RechargeApi
    {
        private Electricity_DAL.RechargeAPI_DAL _rechargeApiDAL = null;
        public RechargeApi(ConnectionStrings connectionString)
        {
            _rechargeApiDAL = new Electricity_DAL.RechargeAPI_DAL(connectionString);
        }
        public async Task<bool> SaveRechargeAPI(string rechargeMode, string operatorName, string apiValue)
        {
            return await this._rechargeApiDAL.SaveRechargeAPI(rechargeMode, operatorName, apiValue);
        }
        /// <summary>
        /// Fetches the API list and API information
        /// </summary>
        /// <param name="rechargeMode"></param>
        /// <returns></returns>
        public async Task<List<RechargeAPI>> GetRechargeAPI(string rechargeMode)
        {
            return await this._rechargeApiDAL.GetRechargeAPI(rechargeMode);
        }

        public async Task<int> InsertTransaction(string userID, string rechargeMode, string rechargeAmount)
        {
            return await this._rechargeApiDAL.InsertTransaction(userID, rechargeMode, rechargeAmount);
        }

        public async Task<bool> UpdateRechargeTransactionStatus(string status, string operatortxnid, string joloorderid, string userorderid, int servicetype)
        {
            return await this._rechargeApiDAL.UpdateRechargeTransactionStatus(status, operatortxnid, joloorderid, userorderid, servicetype);
        }

        public async Task<bool> UpdateTransactionDetails(string orderID, string transactionStatus, string errorMessage)
        {
            return await this._rechargeApiDAL.UpdateTransactionDetails(orderID, transactionStatus, errorMessage);
        }

        public async Task<bool> DeductWalletBalance(string userID, string amount, string message)
        {
            return await this._rechargeApiDAL.DeductWalletBalance(userID, amount, message);
        }

        public async Task<bool> InsertValidationApiDetails(string rechargeMode, string operatorName, string validationApiValue)
        {
            return await this._rechargeApiDAL.InsertValidationApiDetails(rechargeMode, operatorName, validationApiValue);
        }

        public async Task<RechargeAPI> FetchValidationApiDetails(string rechargeMode, string operatorName)
        {
            return await this._rechargeApiDAL.FetchValidationApiDetails(rechargeMode, operatorName);
        }

        public async Task<bool> UpdateRechargeAPI(string rechargeMode, string operatorName, string apiValue)
        {
            return await this._rechargeApiDAL.UpdateRechargeAPI(rechargeMode, operatorName, apiValue);
        }

        public async Task<bool> UpdateValidationApiDetails(string rechargeMode, string operatorName, string validationApiValue)
        {
            return await this._rechargeApiDAL.UpdateValidationApiDetails(rechargeMode, operatorName, validationApiValue);
        }

        public async Task<List<RechargeTransaction>> FetchTransactionHistory(int userID, string startDate, string endDate)
        {
            return await this._rechargeApiDAL.FetchTransactionHistory(userID, startDate, endDate);
        }
        public async Task<List<RechargeTransaction>> FetchTransactionHistory(string startDate, string endDate)
        {
            return await this._rechargeApiDAL.FetchTransactionHistory(startDate, endDate);
        }
        public async Task<bool> AddComplaint(Complaint complaint)
        {
            return await this._rechargeApiDAL.AddComplaint(complaint);
        }

        public async Task<List<Complaint>> FetchUserComplaint(int raisedBy)
        {
            return await this._rechargeApiDAL.FetchUserComplaint(raisedBy);
        }
        public async Task<List<Complaint>> FetchUserComplaintAdmin()
        {
            return await this._rechargeApiDAL.FetchUserComplaintAdmin();
        }

        public async Task<bool> UpdateComplaint(Complaint complaint)
        {
            return await this._rechargeApiDAL.UpdateComplaint(complaint);
        }
    }
}
