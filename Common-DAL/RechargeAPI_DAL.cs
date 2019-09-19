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
                    command.Parameters.Add("@transactionStatus", SqlDbType.VarChar, 10).Value = "FAILURE";
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

        public async Task<bool> UpdateRechargeTransactionStatus(string status, string operatortxnid, string joloorderid, string userorderid, int servicetype)
        {
            string serviceTypeName = "";
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT_INFO_RECHARGE_API_TRANSACTION_DETAILS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeStatus", SqlDbType.VarChar, 20).Value = status;
                    command.Parameters.Add("@operatorTransactionID", SqlDbType.VarChar, 50).Value = operatortxnid;
                    command.Parameters.Add("@joloOrderID", SqlDbType.VarChar, 50).Value = joloorderid;
                    command.Parameters.Add("@userOrderID", SqlDbType.VarChar, 28).Value = userorderid;
                    switch (servicetype)
                    {
                        case 1 :
                            serviceTypeName = "PREPAID";
                            break;
                        case 2:
                            serviceTypeName = "DTH";
                            break;
                        case 4:
                            serviceTypeName = "POSTPAID";
                            break;
                        case 5:
                            serviceTypeName = "LANDLINE";
                            break;
                        case 6:
                            serviceTypeName = "ELECTRICITY";
                            break;
                        case 7:
                            serviceTypeName = "GAS";
                            break;
                        case 8:
                            serviceTypeName = "WATER";
                            break;
                    }
                    command.Parameters.Add("@serviceType", SqlDbType.VarChar, 20).Value = serviceTypeName;
                    command.Parameters.Add("@transactionUpdateTime", SqlDbType.VarChar, 100).Value = DateTime.Now.ToString();
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

        public async Task<bool> UpdateTransactionDetails(string orderID, string transactionStatus, string errorMessage)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE_RECHARGE_TRANSACTION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@transactionID", SqlDbType.Int).Value = Convert.ToInt32(orderID);
                    command.Parameters.Add("@transactionStatus", SqlDbType.VarChar, 10).Value = transactionStatus;
                    command.Parameters.Add("@errorMessage", SqlDbType.VarChar).Value = errorMessage;
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

        public async Task<bool> DeductWalletBalance(string userID, string amount, string message)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DEDUCT_WALLET_BALANCE_RECHARGE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = Convert.ToInt32(userID);
                    command.Parameters.Add("@amount", SqlDbType.Decimal).Value = Convert.ToDecimal(amount);
                    command.Parameters.Add("@message", SqlDbType.VarChar, 100).Value = message;
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

        public async Task<bool> InsertValidationApiDetails(string rechargeMode,string operatorName, string validationApiValue)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT_API_VALIDATION_STATUS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode;
                    command.Parameters.Add("@operatorName", SqlDbType.VarChar, 50).Value = operatorName;
                    command.Parameters.Add("@validationApiValue", SqlDbType.VarChar).Value = validationApiValue;
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
        public async Task<bool> UpdateValidationApiDetails(string rechargeMode, string operatorName, string validationApiValue)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE_VALIDATION_API", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode;
                    command.Parameters.Add("@operatorName", SqlDbType.VarChar, 50).Value = operatorName;
                    command.Parameters.Add("@validationApiValue", SqlDbType.VarChar).Value = validationApiValue;
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

        public async Task<RechargeAPI> FetchValidationApiDetails(string rechargeMode,string operatorName)
        {
            RechargeAPI api = null;
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_API_VALIDATION_STATUS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode;
                    command.Parameters.Add("@operatorName", SqlDbType.VarChar, 50).Value = operatorName;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                api = new RechargeAPI
                                {
                                    RechargeMode = reader["rechargeMode"].ToString(),
                                    OperatorName = reader["operatorName"].ToString(),
                                    ApiValue = reader["validationApiValue"].ToString()
                                };
                                break;
                            }
                        }
                        return api;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<bool> UpdateRechargeAPI(string rechargeMode, string operatorName, string apiValue)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE_RECHARGE_API", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@rechargeMode", SqlDbType.VarChar, 50).Value = rechargeMode.ToUpper();
                    command.Parameters.Add("@operatorName", SqlDbType.VarChar, 50).Value = operatorName.ToUpper();
                    command.Parameters.Add("@apiValue", SqlDbType.VarChar).Value = apiValue;
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
    }
}
