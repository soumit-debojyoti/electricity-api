﻿using Electricity_DAL.Models;
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
            return await this._rechargeApiDAL.InsertTransaction(userID, rechargeMode,rechargeAmount);
        }
    }
}