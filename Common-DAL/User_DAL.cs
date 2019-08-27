using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
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
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 123232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteReaderAsync();
                        {
                            fur.IsUserExist = (bool)command.Parameters["@is_user_exist"].Value;
                            fur.role_id = (int)command.Parameters["@role_id"].Value;
                            fur.message = (string)command.Parameters["@message"].Value;
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

        public async Task<List<TokenDetailsGeneric>> GetUnusedReferalTokenDetails()
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

                    if (user_info.kyc_detail_id > 0)
                    {
                        command.Parameters.Add("@kyc_detail_id", SqlDbType.Int).Value = user_info.kyc_detail_id;
                    }
                    else
                    {
                        command.Parameters.Add("@kyc_detail_id", SqlDbType.Int).Value = DBNull.Value;
                    }
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
                                introducer.Name = Convert.ToString(reader["introname"]).Trim();
                                introducer.SecurityStamp = Convert.ToString(reader["security_stamp"]).Trim();
                                introducer.IntroductionCode = Convert.ToInt64(reader["introcode"]);
                                introducer.JoiningDate = Convert.ToDateTime(reader["used_date"]);
                                introducer.InroducerSecurityStamp = Convert.ToString(reader["user_token_key"]).Trim();
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
    }
}
