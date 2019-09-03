using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_DAL
{
    public class RechargeAPI_DAL
    {
        private string _connectionString = string.Empty;
        public RechargeAPI_DAL(ConnectionStrings connectionString)
        {
            this._connectionString = connectionString.myConnectionString;
        }

        public async Task<bool> SaveRechargeAPI(string rechargeMode, string operatorName, string apiValue)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("RECHARGE_API_INSERT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode.ToUpper();
                    command.Parameters.Add("@operatorName", SqlDbType.VarChar, 50).Value = operatorName.ToUpper();
                    command.Parameters.Add("@apiValue", SqlDbType.VarChar).Value = apiValue;
                    command.Parameters.Add("@apiStatus", SqlDbType.VarChar, 10).Value = "ACTIVE";
                    try
                    {
                        await command.ExecuteReaderAsync();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<List<RechargeAPI>> GetRechargeAPI(string rechargeMode)
        {
            List<RechargeAPI> apiList = new List<RechargeAPI>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_RECHARGE_API_INFO_BY_MODE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode.ToUpper();
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var api = new RechargeAPI
                                {
                                    RechargeMode = reader["rechargeMode"].ToString(),
                                    OperatorName = reader["operatorName"].ToString(),
                                    ApiValue = reader["apiValue"].ToString()
                                };
                                apiList.Add(api);
                            }
                        }
                        return apiList;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<int> InsertTransaction(string userID, string rechargeMode, string rechargeAmount)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT_RECHARGE_TRANSACTION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    command.Parameters.Add("@transactionDate", SqlDbType.VarChar, 20).Value = DateTime.Now.ToShortDateString();
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode.ToUpper();
                    command.Parameters.Add("@rechargeAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(rechargeAmount);
                    command.Parameters.Add("@transactionStatus", SqlDbType.VarChar, 10).Value = "SUCCESS";
                    command.Parameters.Add("@transactionID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        await command.ExecuteReaderAsync();
                        return (int)command.Parameters["@transactionID"].Value;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
