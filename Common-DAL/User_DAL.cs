﻿using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Electricity_DAL
{
    public class User_DAL
    {
        private string _connectionString = string.Empty;
        public User_DAL(ConnectionStrings connectionString)
        {
            this._connectionString = connectionString.myConnectionString;
        }

        public async Task<List<User>> Get_Users()
        {
            List<User> results = new List<User>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_users", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var user = new User
                                {
                                    user_id = Convert.ToInt32(reader["user_id"]),
                                    user_name = Convert.ToString(reader["user_name"]),
                                    email = Convert.ToString(reader["email"]),
                                    security_stamp = Convert.ToString(reader["security_stamp"]),
                                    role_name = Convert.ToString(reader["role_name"]),
                                    role_id = Convert.ToInt32(reader["role_id"])
                                    //,
                                    //first_name = Convert.ToString(reader["first_name"]),
                                    //last_name = Convert.ToString(reader["last_name"]),
                                    //father_name = Convert.ToString(reader["father_name"]),
                                    //dob = Convert.ToDateTime(reader["dob"]).ToString("dd/MM/yyyy"),
                                    //mobile_number = Convert.ToString(reader["mobile_number"]),
                                    //pan_card = Convert.ToString(reader["pan_card"]),
                                    //aadhar_card = Convert.ToString(reader["aadhar_card"]),
                                    //address = Convert.ToString(reader["address"]),
                                    //post_office = Convert.ToString(reader["post_office"]),
                                    //police_station = Convert.ToString(reader["police_station"]),
                                    //district = Convert.ToString(reader["district"]),
                                    //city = Convert.ToString(reader["city"]),
                                    //state_name = Convert.ToString(reader["state_name"]),
                                    //pin = Convert.ToString(reader["pin"]),
                                    //sex = Convert.ToString(reader["sex"])
                                };
                                results.Add(user);
                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return results;
        }

        public async Task<User> Get_User(string user_name)
        {
            User result = null;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result = new User();
                            result.user_id = Convert.ToInt32(reader["user_id"]);
                            result.user_name = Convert.ToString(reader["user_name"]).Trim();
                            result.email = Convert.ToString(reader["email"]).Trim();
                            result.security_stamp = Convert.ToString(reader["security_stamp"]).Trim();
                            result.role_name = Convert.ToString(reader["role_name"]).Trim();
                            result.first_name = Convert.ToString(reader["first_name"]).Trim();
                            result.last_name = Convert.ToString(reader["last_name"]).Trim();
                            result.father_name = Convert.ToString(reader["father_name"]).Trim();
                            result.dob = Convert.ToDateTime(reader["dob"]).ToString("dd/MMM/yyyy").Trim();
                            result.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                            result.pan_card = Convert.ToString(reader["pan_card"]).Trim();
                            result.aadhar_card = Convert.ToString(reader["aadhar_card"]).Trim();
                            result.address = Convert.ToString(reader["address"]).Trim();
                            result.post_office = Convert.ToString(reader["post_office"]).Trim();
                            result.police_station = Convert.ToString(reader["police_station"]).Trim();
                            result.district = Convert.ToString(reader["district"]).Trim();
                            result.city = Convert.ToString(reader["city"]).Trim();
                            result.state_name = Convert.ToString(reader["state_name"]).Trim();
                            result.pin = Convert.ToString(reader["pin"]).Trim();
                            result.sex = Convert.ToString(reader["sex"]).Trim();
                            result.photo = Convert.ToString(reader["photo"]).Trim();
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<TodayUserJoinCountResponse> GetTodayUserJoinCount()
        {
            TodayUserJoinCountResponse result = new TodayUserJoinCountResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("today_joined_user_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@count", SqlDbType.Int, 12);
                    command.Parameters["@count"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            result.count = (int)command.Parameters["@count"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<List<User>> SearchUser(string user_name)
        {
            User result = null;
            List<User> results = new List<User>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("search_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            result = new User();
                            result.user_id = Convert.ToInt32(reader["user_id"]);
                            result.user_name = Convert.ToString(reader["user_name"]).Trim();
                            result.email = Convert.ToString(reader["email"]).Trim();
                            result.security_stamp = Convert.ToString(reader["security_stamp"]).Trim();
                            result.role_name = Convert.ToString(reader["role_name"]).Trim();
                            result.first_name = Convert.ToString(reader["first_name"]).Trim();
                            result.last_name = Convert.ToString(reader["last_name"]).Trim();
                            result.father_name = Convert.ToString(reader["father_name"]).Trim();
                            result.dob = Convert.ToDateTime(reader["dob"]).ToString("dd/MMM/yyyy").Trim();
                            result.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                            result.pan_card = Convert.ToString(reader["pan_card"]).Trim();
                            result.aadhar_card = Convert.ToString(reader["aadhar_card"]).Trim();
                            result.address = Convert.ToString(reader["address"]).Trim();
                            result.post_office = Convert.ToString(reader["post_office"]).Trim();
                            result.police_station = Convert.ToString(reader["police_station"]).Trim();
                            result.district = Convert.ToString(reader["district"]).Trim();
                            result.city = Convert.ToString(reader["city"]).Trim();
                            result.state_name = Convert.ToString(reader["state_name"]).Trim();
                            result.pin = Convert.ToString(reader["pin"]).Trim();
                            result.sex = Convert.ToString(reader["sex"]).Trim();
                            result.photo = Convert.ToString(reader["photo"]).Trim();

                            results.Add(result);
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return results;
        }

        public async Task<MobileValidationResponse> ValidateMobileByMobileNumber(string mobile_number)
        {
            MobileValidationResponse result = new MobileValidationResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("validate_mobile", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = mobile_number;
                    command.Parameters.Add("@bit", SqlDbType.Bit, 1);
                    command.Parameters["@bit"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@user_id", SqlDbType.Int, 12);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            result.isValid = (bool)command.Parameters["@bit"].Value;
                            result.user_id = (int)command.Parameters["@user_id"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<MobileUniqueValidationResponse> ValidateUniqueMobileByMobileNumber(string mobile_number)
        {
            MobileUniqueValidationResponse result = new MobileUniqueValidationResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("validate_unique_mobile", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = mobile_number;
                    command.Parameters.Add("@has_present", SqlDbType.Bit, 1);
                    command.Parameters["@has_present"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 200);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            result.has_present = (bool)command.Parameters["@has_present"].Value;
                            result.message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<AccountValidationResponse> ValidateAccountByUserId(int userid)
        {
            AccountValidationResponse result = new AccountValidationResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("validate_bank_account", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userid;
                    command.Parameters.Add("@isSuccess", SqlDbType.Bit, 1);
                    command.Parameters["@isSuccess"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@message", SqlDbType.VarChar, 500);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            result.isValid = (bool)command.Parameters["@isSuccess"].Value;
                            result.message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<FindUserResponse> Find_Users(string user_name, string password)
        {
            FindUserResponse fur = new FindUserResponse();

            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("find_users", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                    command.Parameters.Add("@is_user_exist", SqlDbType.Bit, 1);
                    command.Parameters["@is_user_exist"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@role_id", SqlDbType.Int, 12);
                    command.Parameters["@role_id"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@user_id", SqlDbType.Int, 12);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 123232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            fur.IsUserExist = (bool)command.Parameters["@is_user_exist"].Value;
                            fur.role_id = command.Parameters["@role_id"].Value == DBNull.Value ? 0 : (int)command.Parameters["@role_id"].Value;
                            fur.user_id = command.Parameters["@user_id"].Value == DBNull.Value ? 0 : (int)command.Parameters["@user_id"].Value;
                            fur.message = command.Parameters["@message"].Value == DBNull.Value ? "no value" : (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return fur;
        }

        public async Task<bool> QualifyUserToRefer(string user_name)
        {
            bool isUserQualify = false;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("qualify_referer_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            int i = Convert.ToInt32(reader[0]);
                            if (i > 0)
                            {
                                isUserQualify = true;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isUserQualify;
        }

        public async Task<string> GetReferelToken(string user_name)
        {
            string referelToken = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_referer_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    command.Parameters.Add("@TOKEN", SqlDbType.NVarChar, 123456);
                    command.Parameters["@TOKEN"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            referelToken = (string)command.Parameters["@TOKEN"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return referelToken;
        }

        public async Task<KYCDetailsResponse> CheckKYCDetail(string user_name)
        {
            KYCDetailsResponse kycResponse = new KYCDetailsResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("check_kyc", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    command.Parameters.Add("@isSuccess", SqlDbType.Bit, 1);
                    command.Parameters["@isSuccess"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 100000000);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            kycResponse.is_success = (bool)command.Parameters["@isSuccess"].Value;
                            kycResponse.message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return kycResponse;
        }

        public async Task<List<KYCDetails>> GetkKYCDetail(int user_id)
        {
            List<KYCDetails> kycs = new List<KYCDetails>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_kyc_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;

                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                KYCDetails kyc = new KYCDetails();
                                kyc.kyc_detail_id = Convert.ToInt32(reader["kyc_detail_id"]);
                                kyc.id_proof_id = Convert.ToInt32(reader["id_proof_id"]);
                                kyc.id_proof_document_path = Convert.ToString(reader["id_proof_document_path"]);
                                kyc.address_proof_id = Convert.ToInt32(reader["address_proof_id"]);
                                kyc.address_proof_document_path = Convert.ToString(reader["address_proof_document_path"]);
                                kyc.bank_details = Convert.ToString(reader["bank_details"]);
                                kyc.created_on = Convert.ToDateTime(reader["created_on"]);
                                kycs.Add(kyc);
                            };
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return kycs;
        }

        public async Task<List<TokenDetailsGeneric>> GetUnusedReferalTokenDetails(int refered_user)
        {
            List<TokenDetailsGeneric> tokens = new List<TokenDetailsGeneric>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_token_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = string.Empty;
                    command.Parameters.Add("@refered_user", SqlDbType.Int).Value = refered_user;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                TokenDetailsGeneric token = new TokenDetailsGeneric();
                                token.token = Convert.ToString(reader["token"]);
                                token.created_date = Convert.ToDateTime(reader["created_date"]);
                                token.is_expired = Convert.ToBoolean(reader["is_expired"]);
                                tokens.Add(token);
                            };
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return tokens;
        }

        public async Task<List<TokenDetailsSpecific>> GetUnusedReferalTokenDetailsByToken(string tokenstring)
        {
            {
                List<TokenDetailsSpecific> tokens = new List<TokenDetailsSpecific>();
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                    using (SqlCommand command = new SqlCommand("get_token_details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@token", SqlDbType.NVarChar).Value = tokenstring;
                        command.Parameters.Add("@refered_user", SqlDbType.NVarChar).Value = string.Empty;
                        try
                        {
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (reader.Read())
                                {
                                    TokenDetailsSpecific token = new TokenDetailsSpecific();
                                    token.token = Convert.ToString(reader["token"]);
                                    token.created_date = Convert.ToDateTime(reader["created_date"]);
                                    token.expiry_date = Convert.ToDateTime(reader["expiry_date"]);
                                    token.token_generator = Convert.ToString(reader["token_generator"]);
                                    token.is_expired = Convert.ToBoolean(reader["is_expired"]);
                                    tokens.Add(token);
                                };
                            }
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }

                    }
                }
                Console.WriteLine("All done. Press any key to finish...");
                return tokens;
            }
        }

        public async Task<ValidateReferalTokenResponse> ValidateReferelToken(string token)
        {
            ValidateReferalTokenResponse objValidateReferalTokenResponse = new ValidateReferalTokenResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("validate_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;
                    //SqlParameter returnParameter=command.Parameters.Add("@bit", SqlDbType.Bit);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add("@bit", SqlDbType.Bit, 1);
                    command.Parameters["@bit"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@introducer_name", SqlDbType.NVarChar, 3456);
                    command.Parameters["@introducer_name"].Direction = ParameterDirection.Output;

                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            objValidateReferalTokenResponse.is_valid = (bool)command.Parameters["@bit"].Value;
                            objValidateReferalTokenResponse.introducer_name = (string)command.Parameters["@introducer_name"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return objValidateReferalTokenResponse;
        }

        public async Task<UserWalletBalanceResponse> GetWalletBalance(int userId)
        {
            UserWalletBalanceResponse objUserWalletBalanceResponse = new UserWalletBalanceResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_balance", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@wallet_count", SqlDbType.Int, 45);
                    command.Parameters["@wallet_count"].Direction = ParameterDirection.Output;

                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            objUserWalletBalanceResponse.WalletBalance = (int)command.Parameters["@wallet_count"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return objUserWalletBalanceResponse;
        }

        public async Task<WalletReportResponse> GetWalletTransactionReport(int userId, string start_date, string end_date)
        {
            WalletReportResponse objWalletReportResponse = new WalletReportResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_balance_report", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@start_date", SqlDbType.NVarChar).Value = end_date;
                    command.Parameters.Add("@end_date", SqlDbType.NVarChar).Value = start_date;
                    List<UserLog> userlogs = new List<UserLog>();
                    List<WalletLog> walletlogs = new List<WalletLog>();
                    List<DateLog> datelogs = new List<DateLog>();
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                UserLog ul = new UserLog();
                                ul.user_id = Convert.ToInt32(reader["user_id"]);
                                ul.user_name = Convert.ToString(reader["user_name"]);
                                userlogs.Add(ul);
                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    WalletLog wl = new WalletLog();
                                    wl.wallet_transaction_id = Convert.ToInt32(reader["wallet_transaction_id"]);
                                    wl.transaction_amount = Convert.ToDouble(reader["transaction_amount"]);
                                    wl.transaction_initiated_user = Convert.ToInt32(reader["transaction_initiated_user"]);
                                    wl.message = Convert.ToString(reader["message"]);
                                    wl.created_on = Convert.ToDateTime(reader["created_on"]);
                                    wl.transaction_mode = Convert.ToString(reader["transaction_mode"]);
                                    walletlogs.Add(wl);
                                }
                            }
                            //if (reader.NextResult())
                            //{
                            //    while (reader.Read())
                            //    {
                            //        DateLog dl = new DateLog();
                            //        dl.month_number = Convert.ToInt32(reader["month_number"]);
                            //        dl.month_name = Convert.ToString(reader["month_name"]);
                            //        dl.year_name = Convert.ToString(reader["year_name"]);
                            //        var fild = datelogs.FindAll(x =>
                            //        {
                            //            if (x.month_name == dl.month_name)
                            //            {
                            //                if (x.year_name == dl.year_name)
                            //                {
                            //                    return true;
                            //                }
                            //                else
                            //                {
                            //                    return false;
                            //                }
                            //            }
                            //            else
                            //            {
                            //                return false;
                            //            }
                            //        });
                            //        if (fild.Count == 0)
                            //        {
                            //            datelogs.Add(dl);
                            //        }
                            //    }
                            //}
                            objWalletReportResponse.user_logs = userlogs;
                            objWalletReportResponse.wallet_logs = walletlogs;
                            //objWalletReportResponse.date_logs = datelogs;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return objWalletReportResponse;
        }

        public async Task<bool> ValidateUserToRefer(string user_name)
        {
            bool isValid = false;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("valid_user_to_refer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    //SqlParameter returnParameter=command.Parameters.Add("@bit", SqlDbType.Bit);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add("@bit", SqlDbType.Bit, 1);

                    command.Parameters["@bit"].Direction = ParameterDirection.Output;

                    await command.ExecuteReaderAsync();
                    {
                        isValid = (bool)command.Parameters["@bit"].Value;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isValid;
        }

        public async Task<bool> ReferUserWithToken(string userId, string token)
        {
            bool isSuccess = false;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("refer_user_with_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userid", SqlDbType.NVarChar).Value = userId;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;
                    //SqlParameter returnParameter=command.Parameters.Add("@bit", SqlDbType.Bit);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add("@bit", SqlDbType.Bit, 0);

                    command.Parameters["@bit"].Direction = ParameterDirection.Output;

                    await command.ExecuteReaderAsync();
                    {
                        isSuccess = (bool)command.Parameters["@bit"].Value;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isSuccess;
        }

        public async Task<int> InsertKYCInfo(KYCDetails kyc_info)
        {
            int bank_detail_id = 0;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_kyc_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id_proof_id", SqlDbType.Int).Value = kyc_info.id_proof_id;
                    command.Parameters.Add("@id_proof_document_path", SqlDbType.NVarChar).Value = kyc_info.id_proof_document_path;
                    command.Parameters.Add("@address_proof_id", SqlDbType.Int).Value = kyc_info.address_proof_id;
                    command.Parameters.Add("@address_proof_document_path", SqlDbType.NVarChar).Value = kyc_info.address_proof_document_path;
                    command.Parameters.Add("@bank_details", SqlDbType.NVarChar).Value = kyc_info.bank_details;

                    command.Parameters.Add("@kyc_detail_id", SqlDbType.Int, 0);
                    command.Parameters["@kyc_detail_id"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            bank_detail_id = (int)command.Parameters["@kyc_detail_id"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return bank_detail_id;
        }

        public async Task<bool> UpdateKYCInfoInUser(int user_id, int kyc_id)
        {
            bool is_success = false;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("update_kyc_details_user", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    command.Parameters.Add("@kyc_id", SqlDbType.Int).Value = kyc_id;

                    command.Parameters.Add("@is_success", SqlDbType.Bit, 0);
                    command.Parameters["@is_success"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            is_success = (bool)command.Parameters["@is_success"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return is_success;
        }
        public async Task<int> InsertBankInfo(BankDetails bank_info)
        {
            int bank_detail_id = 0;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_bank_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@bank_name", SqlDbType.NVarChar).Value = bank_info.bank_name;
                    command.Parameters.Add("@account_holder_name", SqlDbType.NVarChar).Value = bank_info.account_holder_name;
                    command.Parameters.Add("@account_number", SqlDbType.NVarChar).Value = bank_info.account_number;
                    command.Parameters.Add("@ifsc_number", SqlDbType.NVarChar).Value = bank_info.ifsc_number;
                    command.Parameters.Add("@branch_name", SqlDbType.NVarChar).Value = bank_info.branch_name;

                    command.Parameters.Add("@is_pay_online", SqlDbType.Bit).Value = bank_info.is_pay_online == true ? 1 : 0;
                    command.Parameters.Add("@bank_detail_id", SqlDbType.Int, 0);
                    command.Parameters["@bank_detail_id"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            bank_detail_id = (int)command.Parameters["@bank_detail_id"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return bank_detail_id;
        }

        public async Task<string> InsertUserInfo(UserDetails user_info)
        {
            string user_security_stamp = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_user_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_info.username;
                    command.Parameters.Add("@role_id", SqlDbType.Int).Value = user_info.role_id;
                    command.Parameters.Add("@email", SqlDbType.NVarChar).Value = user_info.email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = user_info.password;
                    command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = user_info.first_name;
                    command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = user_info.last_name;
                    command.Parameters.Add("@father_name", SqlDbType.NVarChar).Value = user_info.father_name;
                    command.Parameters.Add("@dob", SqlDbType.NVarChar).Value = user_info.dob;
                    command.Parameters.Add("@mobile_number", SqlDbType.NVarChar).Value = user_info.mobile_number;
                    command.Parameters.Add("@photo_loc", SqlDbType.NVarChar).Value = user_info.photo;
                    command.Parameters.Add("@pan_card", SqlDbType.NVarChar).Value = user_info.pan_card;
                    command.Parameters.Add("@aadhar_card", SqlDbType.NVarChar).Value = user_info.aadhar_card;
                    command.Parameters.Add("@address", SqlDbType.NVarChar).Value = user_info.address;
                    command.Parameters.Add("@post_office", SqlDbType.NVarChar).Value = user_info.post_office;
                    command.Parameters.Add("@police_station", SqlDbType.NVarChar).Value = user_info.police_station;
                    command.Parameters.Add("@district", SqlDbType.NVarChar).Value = user_info.district;
                    command.Parameters.Add("@city", SqlDbType.NVarChar).Value = user_info.city;
                    command.Parameters.Add("@state", SqlDbType.Int).Value = user_info.state;
                    command.Parameters.Add("@pin", SqlDbType.NVarChar).Value = user_info.pin;
                    command.Parameters.Add("@sex", SqlDbType.NVarChar).Value = user_info.sex;
                    command.Parameters.Add("@middle_name", SqlDbType.NVarChar).Value = user_info.middle_name;
                    if (user_info.bank_detail_id > 0)
                    {
                        command.Parameters.Add("@bank_detail_id", SqlDbType.Int).Value = user_info.bank_detail_id;
                    }
                    else
                    {
                        command.Parameters.Add("@bank_detail_id", SqlDbType.Int).Value = DBNull.Value;
                    }
                    command.Parameters.Add("@kyc_detail_id", SqlDbType.Int).Value = user_info.kyc_detail_id;
                    command.Parameters.Add("@introcode", SqlDbType.NVarChar).Value = user_info.introcode != null ? user_info.introcode : "";
                    command.Parameters.Add("@introname", SqlDbType.NVarChar).Value = user_info.introname != null ? user_info.introname : "";

                    command.Parameters.Add("@user_security_stamp", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@user_security_stamp"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            user_security_stamp = (string)command.Parameters["@user_security_stamp"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return user_security_stamp;
        }

        public async Task<RankAchieverModel> GetRankAchieverList(int user_id)
        {
            RankAchieverModel rank = new RankAchieverModel();
            OwnModel o = null;
            ParentModel p = null;
            List<ChildModel> children = new List<ChildModel>();
            List<SiblingModel> siblings = new List<SiblingModel>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_rank_status", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                o = new OwnModel();
                                // His Own Information
                                o.user_name = Convert.ToString(reader["user_name"]).Trim();
                                o.email = Convert.ToString(reader["email"]).Trim();
                                o.role_name = Convert.ToString(reader["role_name"]).Trim();
                                o.first_name = Convert.ToString(reader["first_name"]).Trim();
                                o.middle_name = Convert.ToString(reader["middle_name"]).Trim();
                                o.last_name = Convert.ToString(reader["last_name"]).Trim();
                                o.sex = Convert.ToString(reader["sex"]).Trim();
                                o.father_name = Convert.ToString(reader["father_name"]).Trim();
                                o.dob = Convert.ToDateTime(reader["dob"]);
                                o.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    p = new ParentModel();
                                    // His Parent Information
                                    p.user_name = Convert.ToString(reader["user_name"]).Trim();
                                    p.email = Convert.ToString(reader["email"]).Trim();
                                    p.role_name = Convert.ToString(reader["role_name"]).Trim();
                                    p.first_name = Convert.ToString(reader["first_name"]).Trim();
                                    p.middle_name = Convert.ToString(reader["middle_name"]).Trim();
                                    p.last_name = Convert.ToString(reader["last_name"]).Trim();
                                    p.sex = Convert.ToString(reader["sex"]).Trim();
                                    p.father_name = Convert.ToString(reader["father_name"]).Trim();
                                    p.dob = Convert.ToDateTime(reader["dob"]);
                                    p.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                                };

                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    // His Child Information
                                    ChildModel c = new ChildModel();

                                    c.user_name = Convert.ToString(reader["user_name"]).Trim();
                                    c.email = Convert.ToString(reader["email"]).Trim();
                                    c.role_name = Convert.ToString(reader["role_name"]).Trim();
                                    c.first_name = Convert.ToString(reader["first_name"]).Trim();
                                    c.middle_name = Convert.ToString(reader["middle_name"]).Trim();
                                    c.last_name = Convert.ToString(reader["last_name"]).Trim();
                                    c.sex = Convert.ToString(reader["sex"]).Trim();
                                    c.father_name = Convert.ToString(reader["father_name"]).Trim();
                                    c.dob = Convert.ToDateTime(reader["dob"]);
                                    c.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                                    children.Add(c);
                                };

                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    // His Child Information
                                    SiblingModel s = new SiblingModel();

                                    s.user_name = Convert.ToString(reader["user_name"]).Trim();
                                    s.email = Convert.ToString(reader["email"]).Trim();
                                    s.role_name = Convert.ToString(reader["role_name"]).Trim();
                                    s.first_name = Convert.ToString(reader["first_name"]).Trim();
                                    s.middle_name = Convert.ToString(reader["middle_name"]).Trim();
                                    s.last_name = Convert.ToString(reader["last_name"]).Trim();
                                    s.sex = Convert.ToString(reader["sex"]).Trim();
                                    s.father_name = Convert.ToString(reader["father_name"]).Trim();
                                    s.dob = Convert.ToDateTime(reader["dob"]);
                                    s.mobile_number = Convert.ToString(reader["mobile_number"]).Trim();
                                    siblings.Add(s);
                                };

                            }
                            rank.self = o;
                            rank.parent = p;
                            rank.children = children;
                            rank.siblings = siblings;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return rank;
        }

        public void GetAchieverList(int user_id, int numberOfChild)
        {
            List<AchieverList> firstAchieverList = new List<AchieverList>();
            List<AchieverList> secondAchieverList = new List<AchieverList>();
            //First Acheiver List
            GetFirstAchieverList(user_id, numberOfChild, firstAchieverList);

            //Second Achiever List
            if (firstAchieverList.Count > 0)
            {

                GetSecondAchieverList(user_id, numberOfChild, secondAchieverList);

            }
        }

        public void GetSecondAchieverList(int user_id, int numberOfChild, List<AchieverList> als, bool isChild = false)
        {
            if (user_id > 0)
            {
                Rank rank = GetRank(user_id);
                if (rank.child > 0)
                {
                    if (isChild == true && rank.child >= numberOfChild)
                    {
                        AchieverList achieverList = new AchieverList();
                        achieverList.user_id = user_id;
                        achieverList.rank = 1;
                        als.Add(achieverList);

                    }
                    else
                    {

                    }
                    for (int i = 0; i < rank.ids.Count; i++)
                    {
                        GetSecondAchieverList(rank.ids[i], numberOfChild, als, true);
                    }
                }
            }
        }

        public void GetFirstAchieverList(int user_id, int numberOfChild, List<AchieverList> als)
        {

            if (user_id > 0)
            {
                Rank rank = GetRank(user_id);
                if (rank.child > 0)
                {
                    if (rank.child >= numberOfChild)
                    {
                        AchieverList achieverList = new AchieverList();
                        achieverList.user_id = user_id;
                        achieverList.rank = 1;
                        als.Add(achieverList);
                    }
                    //for (int i=0; i<rank.ids.Count; i++)
                    //{
                    //    GetFirstAchieverList(rank.ids[i], numberOfChild, als);
                    //}
                }
            }
        }





        public Rank GetRank(int user_id)
        {
            Rank r = new Rank();
            List<int> ids = new List<int>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_child_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    command.Parameters.Add("@child", SqlDbType.Int, 0);

                    command.Parameters["@child"].Direction = ParameterDirection.Output;
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int child = (Int32)command.Parameters["@child"].Value;
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["user_id"]);
                                ids.Add(id);
                            };
                            r.child = child;
                            r.ids = ids;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return r;
        }

        public async Task<RankAchieverCountModel> GetRankAchieverListCount(int user_id)
        {
            RankAchieverCountModel rank = new RankAchieverCountModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_rank_count_status", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                rank.childrenCount = Convert.ToInt32(reader["child"]);

                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    rank.siblingsCount = Convert.ToInt32(reader["sibling"]);
                                };

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return rank;
        }

        public async Task<TransferAmountModel> BalanceTransfer(int senderId, int receiverId, decimal amount, string comment)
        {
            TransferAmountModel transfer = new TransferAmountModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_balance_transfer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@sender_id", SqlDbType.Int).Value = senderId;
                    command.Parameters.Add("@receiver_id", SqlDbType.Int).Value = receiverId;
                    command.Parameters.Add("@balance", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@sender_comment", SqlDbType.NVarChar).Value = amount;

                    command.Parameters.Add("@is_rejected", SqlDbType.Bit, 5);
                    command.Parameters["@is_rejected"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@return_message", SqlDbType.NVarChar, 100000);
                    command.Parameters["@return_message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            transfer.is_rejected = (bool)command.Parameters["@is_rejected"].Value;
                            transfer.return_message = (string)command.Parameters["@return_message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return transfer;
        }

        public async Task<bool> FindUser(string user_name)
        {
            int user_count = 0;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("user_exist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user_name;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                user_count = Convert.ToInt32(reader["count"]);

                            };

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return (user_count > 0) ? true : false;
        }

        public async Task<string> RegisterToken(string security_number, string security_stamp_of_new_user)
        {
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("register_user_with_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@security_number", SqlDbType.NVarChar).Value = security_number;
                    command.Parameters.Add("@security_stamp_of_new_user", SqlDbType.NVarChar).Value = security_stamp_of_new_user;

                    command.Parameters.Add("@message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return message;
        }

        public async Task<string> ReactivateToken(string token)
        {
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("reactivate_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;

                    command.Parameters.Add("@message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return message;
        }

        public async Task<string> DeactivateToken(string token)
        {
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("deactivate_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;

                    command.Parameters.Add("@message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return message;
        }

        public async Task<string> SurrenderToken(string token)
        {
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("surrender_token", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;

                    command.Parameters.Add("@message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            message = (string)command.Parameters["@message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return message;
        }

        public int UpdateUserRank(string userSecurityStamp, int userRank = 0)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UpdateUserRank", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserSecurityStamp", SqlDbType.NVarChar).Value = userSecurityStamp;
                    command.Parameters.Add("@UserRank", SqlDbType.Int).Value = userRank;
                    try
                    {
                        var resultCount = command.ExecuteNonQuery();
                        return resultCount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                        // throw ex;
                    }
                }
            }
        }

        public List<RankUser> GetUserSamePeer(string introducerSecurityStamp, int introducerRank)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_REFERED_USER_SAME_LEVEL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@introducerTokenKey", SqlDbType.NVarChar).Value = introducerSecurityStamp;
                    command.Parameters.Add("@introducerRank", SqlDbType.Int).Value = introducerRank;
                    List<RankUser> userInSamePeer = new List<RankUser>();
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new RankUser();
                                result.UserID = Convert.ToInt32(reader["user_id"]);
                                result.UserName = Convert.ToString(reader["user_name"]).Trim();
                                result.Email = Convert.ToString(reader["email"]).Trim();
                                result.SecurityStamp = Convert.ToString(reader["refered_user_token"]).Trim();
                                result.FirstName = Convert.ToString(reader["first_name"]).Trim();
                                result.LastName = Convert.ToString(reader["last_name"]).Trim();
                                result.RoleID = Convert.ToString(reader["role_id"]).Trim();
                                result.UserJoiningDate = Convert.ToDateTime(reader["created_date"]);
                                result.IntroducerCode = Convert.ToString(reader["introcode"]);
                                result.IntroducerSecurityStamp = Convert.ToString(reader["user_token_key"]).Trim();
                                result.UserRank = Convert.ToInt32(reader["userrank"]);
                                userInSamePeer.Add(result);
                            };

                        }
                        return userInSamePeer;
                    }
                    catch (Exception ex)
                    {
                        return null;
                        // throw ex;
                    }
                }
            }
        }

        public Introducer GetIntroducerInfo(string securityStamp)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_USER_INTRODUCER_INFO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserSecurityStamp", SqlDbType.NVarChar).Value = securityStamp;
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var introducer = new Introducer();
                                introducer.UserName = Convert.ToString(reader["user_name"]).Trim();
                                introducer.SecurityStamp = Convert.ToString(reader["security_stamp"]).Trim();
                                introducer.JoiningDate = Convert.ToDateTime(reader["created_on"]);
                                introducer.RoleID = Convert.ToInt32(reader["role_id"]);
                                return introducer;
                            };

                        }
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return null;
                        // throw ex;
                    }
                }
            }
        }

        public RankUser FetchUserRank(string userSecurityStamp)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_USER_RANK", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserSecurityStamp", SqlDbType.NVarChar).Value = userSecurityStamp;
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new RankUser();
                                result.UserRank = Convert.ToInt32(reader["userrank"]);
                                result.UserJoiningDate = Convert.ToDateTime(reader["joiningdate"]);
                                return result;
                            };

                        }
                        return null;
                    }
                    catch (Exception ex)
                    {
                        return null;
                        // throw ex;
                    }
                }
            }
        }

        public List<RankUser> UpdateNextLevel(string userSecurityStamp, int userRank)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_RANK_LIST", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserSecurityStamp", SqlDbType.NVarChar).Value = userSecurityStamp;
                    command.Parameters.Add("@UserRank", SqlDbType.Int).Value = userRank;
                    try
                    {
                        List<RankUser> childList = new List<RankUser>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new RankUser();
                                result.UserRank = Convert.ToInt32(reader["userrank"]);
                                result.UserJoiningDate = Convert.ToDateTime(reader["joiningdate"]);
                                result.IntroducerSecurityStamp = Convert.ToString(reader["user_token_key"]);
                                result.SecurityStamp = Convert.ToString(reader["refered_user_token"]);
                                childList.Add(result);
                            };
                            return childList;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                        // throw ex;
                    }
                }
            }
        }

        public string FetchUserSecurityStamp(int userID)
        {
            string userSecurityStamp = string.Empty;
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GET_SECURITY_INFO_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userSecurityStamp = reader["security_stamp"].ToString().Trim();
                        }
                    }
                }
            }
            return userSecurityStamp;
        }

        public async Task<decimal> FetchReferralBonus(int introducerRank)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_INTRODUCER_BONUS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@introducerLevel", SqlDbType.Int).Value = introducerRank;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToDecimal(reader["bonus_amount"].ToString());
                        }
                        return -1;
                    }
                }
            }
        }

        public async Task<bool> AddWalletBalance(int userID, decimal amount, string message)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("ADD_WALLET_BALANCE", connection))
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

        public async Task UpdateNextLevel(string userSecurityStamp, ConfigurationModel configuration)
        {
            bool response = true;
            await AddReferralBonus(userSecurityStamp);
            while (response)
            {
                Introducer introducer = GetIntroducerInfo(userSecurityStamp);
                response = false;
                if (introducer != null && introducer.RoleID != 4)
                {
                    response = GetUserSamePeer(introducer, configuration.down_side_direct_numer_of_joinee);
                    userSecurityStamp = introducer.SecurityStamp;
                    /* Add introducer bonus for succssfull referral */
                    //var bonusAmount = FetchReferralBonus(
                    //    FetchUserRank(introducer.SecurityStamp).UserRank).Result;
                    //if (bonusAmount > 0)
                    //{
                    //    int introducerID = (Get_User(introducer.UserName).Result).user_id;
                    //    await AddWalletBalance(introducerID, bonusAmount, "Referral bonus");
                    //}
                }
            }

        }
        public async Task AddReferralBonus(string userSecurityStamp)
        {
            int count = 1;
            bool response = true;
            while (response && count <= 12)
            {
                Introducer introducer = GetIntroducerInfo(userSecurityStamp);
                // response = false;
                if (introducer != null && introducer.RoleID != 4)
                {
                    /* Add introducer bonus for succssfull referral */
                    var bonusAmount = FetchReferralBonus(
                        count).Result;
                    if (bonusAmount > 0)
                    {
                        int introducerID = (Get_User(introducer.UserName).Result).user_id;
                        await AddWalletBalance(introducerID, bonusAmount, "Referral bonus");
                    }
                    userSecurityStamp = introducer.SecurityStamp;
                    count++;
                }
                else
                {
                    response = false;
                }
            }

        }
        /// <summary>
        /// Check user count and update rank for introducer
        /// </summary>
        /// <param name="user_security_stamp"></param>
        /// <param name="introducerRank"></param>
        /// <returns></returns>
        public bool GetUserSamePeer(Introducer introducer, int count)
        {
            bool isIntroducerRankUpdated = false;
            int levelCount = count;
            //Introducer introducer = _user.GetIntroducerInfo(user_security_stamp);
            if (introducer != null && introducer.RoleID != 4)
            {
                var introducerRank = 0;
                //rs.UpdateUserRank(introducer.SecurityStamp, 0);
                if (FetchUserRank(introducer.SecurityStamp) != null)
                {
                    introducerRank = FetchUserRank(introducer.SecurityStamp).UserRank;
                }
                else
                {
                    UpdateUserRank(introducer.SecurityStamp, 0);

                }

                // 1st Level
                if (introducerRank == 0)
                {
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 1);
                        isIntroducerRankUpdated = true;
                    }
                    else
                    {
                        UpdateUserRank(introducer.SecurityStamp, 0);
                    }
                }
                // 2nd Level
                else if (introducerRank == 2)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 2);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 3rd Level
                else if (introducerRank == 2)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 3);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 4th Level
                else if (introducerRank == 3)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 4);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 5th Level
                else if (introducerRank == 4)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 5);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 6th Level
                else if (introducerRank == 5)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 6);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 7th Level
                else if (introducerRank == 6)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 7);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 8th Level
                else if (introducerRank == 7)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 8);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 9th Level
                else if (introducerRank == 8)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 9);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 10th Level
                else if (introducerRank == 9)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 10);
                        isIntroducerRankUpdated = true;
                    }
                }

                //11th Level
                else if (introducerRank == 10)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 11);
                        isIntroducerRankUpdated = true;
                    }
                }

                // 12th Level
                else if (introducerRank == 11)
                {
                    //
                    if (GetUserSamePeer(introducer.SecurityStamp, introducerRank).Count >= levelCount)
                    {
                        UpdateUserRank(introducer.SecurityStamp, 12);
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
                return FetchUserRank(
               FetchUserSecurityStamp(userID)).UserRank;
            }
            catch
            {

                return 0;
            }

        }

        public async Task<List<WalletTransaction>> FetchAllWalletTransaction(int requestorID, DateTime startDate, DateTime endDate)
        {
            List<WalletTransaction> walletTransactionList = new List<WalletTransaction>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_ALL_TRANSACTION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@requestorID", SqlDbType.Int).Value = requestorID;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate;
                    command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate.AddDays(1);

                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var walletTransaction = new WalletTransaction();
                                walletTransaction.WalletTransactionID = Convert.ToInt32(reader["wallet_transaction_id"].ToString());
                                walletTransaction.TransactionAmount = Convert.ToDecimal(reader["transaction_amount"].ToString());
                                walletTransaction.UserName = reader["user_name"].ToString();
                                walletTransaction.TransactionMessage = reader["message"].ToString();
                                walletTransaction.TransactionTime = Convert.ToDateTime(reader["created_on"].ToString());
                                walletTransaction.TransactionMode = reader["transaction_mode"].ToString();
                                walletTransaction.TransactionByFirstName = reader["first_name"].ToString();
                                walletTransaction.TransactionByLastName = reader["last_name"].ToString();
                                walletTransaction.MobileNumber = reader["mobile_number"].ToString();
                                walletTransaction.UserID = Convert.ToInt32(reader["user_id"].ToString());
                                walletTransactionList.Add(walletTransaction);
                            }
                            return walletTransactionList;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<RankAcheiver>> FetchAllRankAcheiver()
        {
            List<RankAcheiver> rankAcheivers = new List<RankAcheiver>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_ALL_RANK_ACHIEVER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var rankAcheiver = new RankAcheiver();
                                rankAcheiver.UserName = reader["user_name"].ToString();
                                rankAcheiver.FirstName = reader["first_name"].ToString();
                                rankAcheiver.LastName = reader["last_name"].ToString();
                                rankAcheiver.UserRank = Convert.ToInt32(reader["userrank"].ToString());
                                rankAcheiver.RankAchievementDate = Convert.ToDateTime(reader["lastmodified"].ToString());
                                rankAcheiver.JoiningDate = Convert.ToDateTime(reader["joiningdate"].ToString());

                                rankAcheivers.Add(rankAcheiver);
                            }
                            return rankAcheivers;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


        public async Task<List<IntroducerBonus>> FetchAllReferralBonus()
        {
            List<IntroducerBonus> introducerBonusInfo = new List<IntroducerBonus>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_ALL_INTRODUCER_BONUS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var info = new IntroducerBonus();
                            info.IntroducerLevel = Convert.ToInt32(reader["introducer_level"].ToString());
                            info.ReferralBonus = Convert.ToDecimal(reader["bonus_amount"].ToString());
                            info.MonthlyPayout = Convert.ToDecimal(reader["monthly_payout"].ToString());
                            introducerBonusInfo.Add(info);
                        }
                        return introducerBonusInfo;
                    }
                }
            }
        }

        public async Task<bool> UpdateIntroducerReferralBonus(int userLevel, decimal referralAmount, decimal monthlyAmount)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE_INTRODUCER_REFERRAL_BONUS_INFO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@introducer_level", SqlDbType.Int).Value = userLevel;
                    command.Parameters.Add("@bonus_amount", SqlDbType.Decimal).Value = referralAmount;
                    command.Parameters.Add("@monthly_payout", SqlDbType.Decimal).Value = monthlyAmount;
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<List<RegisterUser>> GetAllUsersDetails()
        {
            List<RegisterUser> results = new List<RegisterUser>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("FETCH_ALL_USERS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var user = new RegisterUser
                                {
                                    firstName = Convert.ToString(reader["first_name"]),
                                    username = Convert.ToString(reader["user_name"]),
                                    email = Convert.ToString(reader["email"]),
                                    lastName = Convert.ToString(reader["last_name"]),
                                    gender = Convert.ToString(reader["sex"]).ToLower() == "male" ? 1 : 0,
                                    dob = Convert.ToString(reader["dob"]),
                                    address = Convert.ToString(reader["address"]),
                                    po = Convert.ToString(reader["post_office"]),
                                    ps = Convert.ToString(reader["police_station"]),
                                    district = Convert.ToString(reader["district"]),
                                    city = Convert.ToString(reader["city"]),
                                    state = Convert.ToInt32(reader["state"]),
                                    pincode = Convert.ToString(reader["pin"]),
                                    mobile = Convert.ToString(reader["mobile_number"]),
                                    password = Convert.ToString(reader["password"]),

                                    //,
                                    //first_name = Convert.ToString(reader["first_name"]),
                                    //last_name = Convert.ToString(reader["last_name"]),
                                    //father_name = Convert.ToString(reader["father_name"]),
                                    //dob = Convert.ToDateTime(reader["dob"]).ToString("dd/MM/yyyy"),
                                    //mobile_number = Convert.ToString(reader["mobile_number"]),
                                    //pan_card = Convert.ToString(reader["pan_card"]),
                                    //aadhar_card = Convert.ToString(reader["aadhar_card"]),
                                    //address = Convert.ToString(reader["address"]),
                                    //post_office = Convert.ToString(reader["post_office"]),
                                    //police_station = Convert.ToString(reader["police_station"]),
                                    //district = Convert.ToString(reader["district"]),
                                    //city = Convert.ToString(reader["city"]),
                                    //state_name = Convert.ToString(reader["state_name"]),
                                    //pin = Convert.ToString(reader["pin"]),
                                    //sex = Convert.ToString(reader["sex"])
                                };
                                results.Add(user);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        }
                    }
                }
            }
            return results;
        }

        public async Task<RegisterUser> GetAllUsersDetails(int userID)
        {
            RegisterUser user = null;
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_USER_DETAILS_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userID;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                user = new RegisterUser
                                {
                                    firstName = Convert.ToString(reader["first_name"]),
                                    username = Convert.ToString(reader["user_name"]),
                                    email = Convert.ToString(reader["email"]),
                                    lastName = Convert.ToString(reader["last_name"]),
                                    gender = Convert.ToString(reader["sex"]).ToLower() == "male" ? 1 : 0,
                                    dob = Convert.ToString(reader["dob"]),
                                    address = Convert.ToString(reader["address"]),
                                    po = Convert.ToString(reader["post_office"]),
                                    ps = Convert.ToString(reader["police_station"]),
                                    district = Convert.ToString(reader["district"]),
                                    city = Convert.ToString(reader["city"]),
                                    state = Convert.ToInt32(reader["state"]),
                                    pincode = Convert.ToString(reader["pin"]),
                                    mobile = Convert.ToString(reader["mobile_number"]),
                                    password = Convert.ToString(reader["password"])
                                };

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return null;
                            }

                        }
                    }
                }
            }
            return user;
        }

        public async Task<bool> UpdateUserDetails(int userID, RegisterUser user_info)
        {
            string user_security_stamp = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("UPDATE_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = user_info.email;
                    command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = user_info.password;
                    command.Parameters.Add("@firstName", SqlDbType.VarChar, 50).Value = user_info.firstName;
                    command.Parameters.Add("@lastName", SqlDbType.VarChar, 50).Value = user_info.lastName;
                    command.Parameters.Add("@dob", SqlDbType.DateTime).Value = Convert.ToDateTime(user_info.dob);
                    command.Parameters.Add("@mobileNumber", SqlDbType.NVarChar).Value = user_info.mobile;
                    command.Parameters.Add("@address", SqlDbType.NVarChar).Value = user_info.address;
                    command.Parameters.Add("@postOffice", SqlDbType.NVarChar).Value = user_info.po;
                    command.Parameters.Add("@policeStation", SqlDbType.NVarChar).Value = user_info.ps;
                    command.Parameters.Add("@district", SqlDbType.NVarChar).Value = user_info.district;
                    command.Parameters.Add("@city", SqlDbType.NVarChar).Value = user_info.city;
                    command.Parameters.Add("@state", SqlDbType.Int).Value = user_info.state;
                    switch (user_info.gender)
                    {
                        case 0:
                            command.Parameters.Add("@sex", SqlDbType.NVarChar).Value = "Male";
                            break;
                        case 1:
                            command.Parameters.Add("@sex", SqlDbType.NVarChar).Value = "Female";
                            break;
                    }
                    command.Parameters.Add("@pin", SqlDbType.NVarChar).Value = user_info.pincode;

                    try
                    {
                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }

                }
            }
        }

        public async Task<CommissionSetting> FetchCommissionAmount(string rechargeType, string operatorName, decimal transactionAmount)
        {
            CommissionSetting cs = null;
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("calculate_commission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@recharge_type", SqlDbType.VarChar, 100).Value = rechargeType;
                    command.Parameters.Add("@operator_name", SqlDbType.VarChar, 100).Value = operatorName;
                    command.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = transactionAmount;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                cs = new CommissionSetting
                                {
                                    WalletMode = Convert.ToInt32(reader["wallet_mode"]),
                                    Amount = Convert.ToDecimal(reader["amount"])
                                };
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                cs = null;
                            }

                        }
                    }

                }
            }
            return cs;
        }

        public async Task<decimal> FetchLevelBonusRecharge(string rechargeType, string operatorName, decimal transactionAmount)
        {
            decimal levelCommission = 0;
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("CALCULATE_LEVEL_COMMISSION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@recharge_type", SqlDbType.VarChar, 100).Value = rechargeType;
                    command.Parameters.Add("@operator_name", SqlDbType.VarChar, 100).Value = operatorName;
                    command.Parameters.Add("@transaction_amount", SqlDbType.Decimal).Value = transactionAmount;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                levelCommission = Convert.ToDecimal(reader["level_commission"]);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return 0;
                            }

                        }
                        return levelCommission;
                    }

                }
            }
        }

        public async Task<bool> PayOutRechargeBonus(int userID, string rechargeType, string operatorName, decimal transactionAmount)
        {
            // Wallet Mode :: 0 - deduct: service charge || 1 - add: commission
            CommissionSetting csObject = new CommissionSetting();
            decimal levelCommission = 0;
            try
            {
                csObject = await FetchCommissionAmount(rechargeType, operatorName, transactionAmount);
                if (csObject.WalletMode == 1)
                {
                    await AddWalletBalance(userID, csObject.Amount, "Bill payment commission added, for - " + operatorName);
                }
                if (csObject.WalletMode == 0)
                {
                    await AddWalletBalance(userID, -csObject.Amount, "Bill payment service charge deducted, for - " + operatorName);
                }
                levelCommission = await FetchLevelBonusRecharge(rechargeType, operatorName, transactionAmount);
                await AddLevelBonus(FetchUserSecurityStamp(userID), levelCommission);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task AddLevelBonus(string userSecurityStamp, decimal bonusAmount)
        {
            bool response = true;
            int count = 1;
            while (response && count <= 12)
            {
                Introducer introducer = GetIntroducerInfo(userSecurityStamp);
                // response = false;
                if (introducer != null && introducer.RoleID != 4)
                {
                    /* Add introducer bonus for succssfull referral */
                    if (bonusAmount > 0)
                    {
                        int introducerID = (Get_User(introducer.UserName).Result).user_id;
                        await AddWalletBalance(introducerID, bonusAmount, "Level recharge bonus added.");
                    }
                    userSecurityStamp = introducer.SecurityStamp;
                    count++;
                }
                else
                {
                    response = false;
                }
            }

        }

        public async Task<bool> AddCommissionSetting(string rechargeType, string operatorName, int commissionType, int calculationType, decimal value, int levelPayoutType, decimal lValue)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("insert_commission_setting_details", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@recharge_type", SqlDbType.VarChar, 100).Value = rechargeType;
                    command.Parameters.Add("@operator_name", SqlDbType.VarChar, 100).Value = operatorName;
                    command.Parameters.Add("@commission_type", SqlDbType.Int).Value = commissionType;
                    command.Parameters.Add("@calculation_type", SqlDbType.Int).Value = calculationType;
                    command.Parameters.Add("@value", SqlDbType.Decimal).Value = value;
                    command.Parameters.Add("@level_payout_type", SqlDbType.Int).Value = levelPayoutType;
                    command.Parameters.Add("@level_payout_value", SqlDbType.Decimal).Value = lValue;
                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }
        public async Task<List<CommissionSettingValue>> FetchAllCommissionsAdded()
        {
            List<CommissionSettingValue> csList = new List<CommissionSettingValue>();
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("FETCH_COMMISSION_SETTING", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                var cs = new CommissionSettingValue
                                {
                                    OperatorName = Convert.ToString(reader["operator_name"]),
                                    RechargeType = reader["recharge_type"].ToString(),
                                    CommissionType = Convert.ToInt32(reader["commission_type"].ToString()) == 0 ? "Service" : "Commission",
                                    CalculationType = Convert.ToInt32(reader["tc_type"].ToString()) == 1 ? "Amount" : "Percentage",
                                    LevelPayoutType = Convert.ToInt32(reader["lc_type"].ToString()) == 1 ? "Amount" : "Percentage",
                                    Value = Convert.ToInt32(reader["tc_type"].ToString()) == 1 ? Convert.ToDecimal(reader["tc_a_value"].ToString()) : Convert.ToDecimal(reader["tc_p_value"].ToString()),
                                    LevelPayoutValue = Convert.ToInt32(reader["lc_type"].ToString()) == 1 ? Convert.ToDecimal(reader["lc_a_value"].ToString()) : Convert.ToDecimal(reader["lc_p_value"].ToString()),
                                    T_ID = Convert.ToInt32(reader["tc_id"].ToString()),
                                    L_ID = Convert.ToInt32(reader["lc_id"].ToString()),
                                    Ref_CS_ID = Convert.ToInt32(reader["ref_cs_id"].ToString()),
                                    CS_ID = Convert.ToInt32(reader["cs_id"].ToString()),
                                };
                                csList.Add(cs);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return null;
                            }

                        }
                        return csList;
                    }
                }
            }
        }
    }
}
