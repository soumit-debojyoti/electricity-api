using Electricity_DAL.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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

        public async Task<int> InsertTransaction(string userID, string rechargeMode, string rechargeAmount, string serviceNumber)
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
                    command.Parameters.Add("@serviceNumber", SqlDbType.NVarChar, 50).Value = serviceNumber;
                    
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

        public async Task<List<RechargeTransaction>> FetchTransactionHistory(int userID, string startDate, string endDate)
        {
            List<RechargeTransaction> transactions = new List<RechargeTransaction>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_TRANSACTION_HISTORY", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = Convert.ToDateTime(startDate).Date;
                    command.Parameters.Add("@endDate", SqlDbType.Date).Value = Convert.ToDateTime(endDate).Date.AddDays(1);
                    
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var transaction = new RechargeTransaction
                                {
                                    TransactionID = Convert.ToInt32(reader["transaction_id"].ToString()),
                                    UserID = Convert.ToInt32(reader["userID"].ToString()),
                                    TransactionDate = Convert.ToDateTime(reader["transactionDate"].ToString()),
                                    TransactionMode = reader["rechargeMode"].ToString(),
                                    TransactionStatus = reader["transactionStatus"].ToString(),
                                    TransactionMessage = reader["errorMessage"].ToString(),
                                    TransactionAmount = Convert.ToDecimal(reader["rechargeAmount"].ToString()),
                                    ServiceNumber = reader["service_number"].ToString()
                                };
                                transactions.Add(transaction);
                            }
                        }
                        return transactions = transactions.OrderByDescending( o => o.TransactionDate).ToList();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<RechargeTransaction>> FetchTransactionHistory(string startDate, string endDate)
        {
            List<RechargeTransaction> transactions = new List<RechargeTransaction>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_ALL_TRANSACTION_HISTORY", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = Convert.ToDateTime(startDate).Date;
                    command.Parameters.Add("@endDate", SqlDbType.Date).Value = Convert.ToDateTime(endDate).Date;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var transaction = new RechargeTransaction
                                {
                                    TransactionID = Convert.ToInt32(reader["transaction_id"].ToString()),
                                    UserID = Convert.ToInt32(reader["userID"].ToString()),
                                    TransactionDate = Convert.ToDateTime(reader["transactionDate"].ToString()).Date,
                                    TransactionMode = reader["rechargeMode"].ToString(),
                                    TransactionStatus = reader["transactionStatus"].ToString(),
                                    TransactionMessage = reader["errorMessage"].ToString(),
                                    TransactionAmount = Convert.ToDecimal(reader["rechargeAmount"].ToString()),
                                    ServiceNumber = reader["service_number"].ToString(),
                                    APIResponse = reader["api_response"].ToString()
                                };
                                transactions.Add(transaction);
                            }
                        }                        
                        return transactions = transactions.OrderByDescending(x => x.TransactionID).ToList(); ;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<bool> AddComplaint(Complaint complaint)
        {
            string serviceTypeName = "";
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("ADD_USER_COMPLAINT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@tid", SqlDbType.Int).Value = complaint.TID;
                    command.Parameters.Add("@cpriority", SqlDbType.Int).Value = complaint.CPriority;
                    command.Parameters.Add("@raisedby", SqlDbType.Int).Value = complaint.RaisedBy;
                    command.Parameters.Add("@user_contact", SqlDbType.VarChar, 10).Value = complaint.UserContactNumber;
                    command.Parameters.Add("@user_comment", SqlDbType.VarChar, 250).Value = complaint.UserComment;
                    command.Parameters.Add("@cstatus", SqlDbType.Int).Value = complaint.CStatus;
                    command.Parameters.Add("@resolver_comment", SqlDbType.VarChar, 250).Value = complaint.ResolverComment;
                    command.Parameters.Add("@assinged_to", SqlDbType.Int).Value = complaint.AssignedTo;
                    command.Parameters.Add("@resolved_by", SqlDbType.Int).Value = complaint.ResolvedBy;
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

        public async Task<List<Complaint>> FetchUserComplaint(int raisedBy)
        {
            List<Complaint> complaints = new List<Complaint>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_USER_COMPLAINT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@raisedBy", SqlDbType.Int).Value = raisedBy;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var complaint = new Complaint
                                {
                                    CID = Convert.ToInt32(reader["cid"].ToString()),
                                    TID = Convert.ToInt32(reader["tid"].ToString()),
                                    CPriority = Convert.ToInt32(reader["cpriority"].ToString()),
                                    UserContactNumber = reader["user_contact"].ToString(),
                                    UserComment = reader["user_comment"].ToString(),
                                    CStatus = Convert.ToInt32(reader["cstatus"].ToString()),
                                    ResolverComment = reader["resolver_comment"].ToString(),
                                    AssignedToName = reader["user_name"].ToString(),
                                };
                                switch (Convert.ToInt32(reader["cpriority"].ToString()))
                                {
                                    case 1: complaint.CPriorityText = "Low";
                                        break;
                                    case 2: complaint.CPriorityText = "Medium";
                                        break;
                                    case 3:
                                        complaint.CPriorityText = "High";
                                        break;
                                }
                                switch (Convert.ToInt32(reader["cstatus"].ToString()))
                                {
                                    case 1:
                                        complaint.CStatusText = "Acknowledged";
                                        break;
                                    case 2:
                                        complaint.CStatusText = "Assigned";
                                        break;
                                    case 3:
                                        complaint.CStatusText = "Work In Progress";
                                        break;
                                    case 4:
                                        complaint.CStatusText = "Resolved";
                                        break;
                                }
                                complaints.Add(complaint);
                            }
                        }
                        return complaints;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        public async Task<List<Complaint>> FetchUserComplaintAdmin()
        {
            List<Complaint> complaints = new List<Complaint>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_USER_COMPLAINT_ADMIN", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var complaint = new Complaint
                                {
                                    CID = Convert.ToInt32(reader["cid"].ToString()),
                                    RaisedBy = Convert.ToInt32(reader["raisedby"].ToString()),
                                    RaisedByText = reader["raisedBy"].ToString(),
                                    TID = Convert.ToInt32(reader["tid"].ToString()),
                                    CPriority = Convert.ToInt32(reader["cpriority"].ToString()),
                                    UserContactNumber = reader["user_contact"].ToString(),
                                    UserComment = reader["user_comment"].ToString(),
                                    CStatus = Convert.ToInt32(reader["cstatus"].ToString()),
                                    ResolverComment = reader["resolver_comment"].ToString(),
                                    AssignedToName = reader["user_name"].ToString(),
                                };
                                var transaction = new RechargeTransaction
                                {
                                    TransactionMessage = reader["errorMessage"].ToString(),
                                    TransactionAmount = Convert.ToDecimal(reader["rechargeAmount"].ToString()),
                                    TransactionDate = Convert.ToDateTime(reader["transactionDate"].ToString()).Date,
                                    TransactionMode = reader["rechargeMode"].ToString(),
                                    TransactionStatus = reader["transactionStatus"].ToString()
                                };
                                complaint.Transaction = transaction;
                                switch (Convert.ToInt32(reader["cpriority"].ToString()))
                                {
                                    case 1:
                                        complaint.CPriorityText = "Low";
                                        break;
                                    case 2:                                        
                                        complaint.CPriorityText = "Medium";
                                        break;
                                    case 3:
                                        complaint.CPriorityText = "High";
                                        break;
                                }
                                switch (Convert.ToInt32(reader["cstatus"].ToString()))
                                {
                                    case 1:
                                        complaint.CStatusText = "Acknowledged";
                                        break;
                                    case 2:
                                        complaint.CStatusText = "Assigned";
                                        break;
                                    case 3:
                                        complaint.CStatusText = "Work In Progress";
                                        break;
                                    case 4:
                                        complaint.CStatusText = "Resolved";
                                        break;
                                }
                                complaints.Add(complaint);
                            }
                        }
                        return complaints;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<bool> UpdateComplaint(Complaint complaint)
        {
            string serviceTypeName = "";
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE_USER_COMPLAINT", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@cid", SqlDbType.Int).Value = complaint.CID;
                    command.Parameters.Add("@cstatus", SqlDbType.Int).Value = complaint.CStatus;
                    command.Parameters.Add("@resolver_comment", SqlDbType.VarChar, 250).Value = complaint.ResolverComment;
                    command.Parameters.Add("@assinged_to", SqlDbType.Int).Value = complaint.AssignedTo;
                    command.Parameters.Add("@resolved_by", SqlDbType.Int).Value = complaint.ResolvedBy;
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

        public string FormPrepaidAPIString(string apiValue, dynamic rechargeObject)
        {
            apiValue = apiValue.Replace("#Mobile#", rechargeObject.mobileNumber.Value);
            apiValue = apiValue.Replace("#Amount#", rechargeObject.amount.Value);
            apiValue = apiValue.Replace("#Order#", rechargeObject.orderNumber.Value);
            return apiValue;

        }

        public async Task<JObject> Recharge(string rechargetype, dynamic rechargeObject)
        {
            JObject json = null;
            try
            {
                List<RechargeAPI> apiList = await this.GetRechargeAPI(rechargetype);
                string apiValue = apiList.Find(x => x.OperatorName == rechargeObject.operatorName.Value).ApiValue;
                if (rechargetype.ToUpper() == "PREPAID" || rechargetype.ToUpper() == "DTH")
                {
                    apiValue = FormPrepaidAPIString(apiValue, rechargeObject);
                }

                if (rechargetype.ToUpper() == "ELECTRICITY" || rechargetype.ToUpper() == "GAS" || rechargetype.ToUpper() == "WATER")
                {
                    apiValue = FormUtilityAPIString(apiValue, rechargeObject);
                }

                if(rechargetype.ToUpper() == "POSTPAID")
                {
                    apiValue = FormPostpaidAPIString(apiValue, rechargeObject);
                }
                
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    var response = httpClient.GetStringAsync(new Uri(apiValue)).Result;
                    json = JObject.Parse(response);
                    await InsertAPIResponse(Convert.ToInt32(rechargeObject.orderNumber.Value), response);
                    // var releases = JArray.Parse(response);
                }
                return json;
            }
            catch(Exception ex)
            {
                JObject errorJson = JObject.Parse(ex.Message);
                return errorJson;
            }
            
        }
        private async Task InsertAPIResponse(int transactionID, string apiResponse)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT_RECHARGE_API_RESPONSE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@transactionID", SqlDbType.Int).Value = transactionID;
                    command.Parameters.Add("@apiResponse", SqlDbType.VarChar).Value = apiResponse;
                    try
                    {
                        await command.ExecuteReaderAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public async Task<JObject> ValidateUtilityRecharge(string rechargetype, string operatorName, string consumer_number, string customer_mobile)
        {
            JObject json = null;
            RechargeAPI api = new RechargeAPI();
            try
            {
                api = await this.FetchValidationApiDetails(rechargetype, operatorName);
                api.ApiValue = FormUtilityValidtaionAPIString(api.ApiValue, consumer_number, customer_mobile);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    var response = httpClient.GetStringAsync(new Uri(api.ApiValue)).Result;
                    json = JObject.Parse(response);
                    // var releases = JArray.Parse(response);
                }
                return json;
            }
            catch (Exception ex)
            {
                JObject errorJson = JObject.Parse(ex.Message);
                return errorJson;
            }
        }

        public string FormUtilityValidtaionAPIString(string apiValue, string consumer_number, dynamic customer_mobile)
        {
            apiValue = apiValue.Replace("#consumer_number#", consumer_number);
            apiValue = apiValue.Replace("#customer_mobile#", customer_mobile);
            apiValue = apiValue.Replace("#order#", "001");
            apiValue = apiValue.Replace("#customer_name#", "XX");
            return apiValue;
        }

        public string FormUtilityAPIString(string apiValue, dynamic rechargeObject)
        {
            apiValue = apiValue.Replace("#consumer_number#", rechargeObject.consumerNumber.Value);
            apiValue = apiValue.Replace("#amount#", rechargeObject.amount.Value);
            apiValue = apiValue.Replace("#order#", rechargeObject.orderNumber.Value);
            apiValue = apiValue.Replace("#customer_mobile#", rechargeObject.customerMobileNumber.Value);
            apiValue = apiValue.Replace("#customer_name#", rechargeObject.customerName.Value);
            apiValue = apiValue.Replace("#reference_id#", rechargeObject.validationReferenceID.Value);
            return apiValue;

        }

        public string FormPostpaidAPIString(string apiValue, dynamic rechargeObject)
        {
            apiValue = apiValue.Replace("#mobileno#", rechargeObject.rechargeMobileNumber.Value);
            apiValue = apiValue.Replace("#amount#", rechargeObject.amount.Value);
            apiValue = apiValue.Replace("#orderid#", rechargeObject.orderNumber.Value);
            apiValue = apiValue.Replace("#customermobile#", rechargeObject.customerMobileNumber.Value);
            apiValue = apiValue.Replace("#customername#", rechargeObject.customerName.Value);
            return apiValue;

        }



        //public async Task PayRechargeCommission(string rechargetype, dynamic rechargeObject)
        //{
        //    bool payMode = false; // :: 0 - Deduct && 1 - Add
        //    double amount = 0;

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(this._connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("calculate_commission", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@recharge_type", SqlDbType.VarChar, 100).Value = rechargetype;
        //                command.Parameters.Add("@operator_name", SqlDbType.VarChar, 100).Value = rechargeObject.operatorName.Value;
        //                command.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = rechargeObject.amount.Value;
        //                try
        //                {
        //                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            payMode = Convert.ToBoolean(reader["wallet_mode"].ToString());
        //                            amount = Convert.ToDouble(reader["amount"].ToString());
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
    }
}
