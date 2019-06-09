using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Electricity_DAL
{
    public class Common_DAL
    {
        private string _connectionString = string.Empty;
        public Common_DAL(ConnectionStrings connectionString)
        {
            this._connectionString = connectionString.myConnectionString;
        }


        public async Task<List<AddressProof>> GetAddressProof()
        {
            List<AddressProof> addressproofs = new List<AddressProof>();
            AddressProof addressproof = null;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_address_proof", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            addressproof = new AddressProof();
                            addressproof.address_proof_id = Convert.ToInt32(reader["address_proof_id"]);
                            addressproof.address_proof_type = Convert.ToString(reader["address_proof_type"]).Trim();
                            addressproofs.Add(addressproof);
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return addressproofs;
        }

        public async Task<List<IdProof>> GetIdProof()
        {
            List<IdProof> idproofs = new List<IdProof>();
            IdProof idproof = null;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_id_proof", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            idproof = new IdProof();
                            idproof.id_proof_id = Convert.ToInt32(reader["id_proof_id"]);
                            idproof.id_proof_type = Convert.ToString(reader["proof_type"]).Trim();
                            idproofs.Add(idproof);
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return idproofs;
        }

        public async Task<List<State>> GetStates()
        {
            List<State> states = new List<State>();
            State state = null;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_states", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            state = new State();
                            state.state_id = Convert.ToInt32(reader["state_id"]);
                            state.state_name = Convert.ToString(reader["state_name"]).Trim();
                            states.Add(state);
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return states;
        }

        public async Task<PagePermissionResponse> PagePermission(string roleId)
        {
            PagePermissionResponse p = new PagePermissionResponse();
            List<PagePermissionModel> ppmEdit = new List<PagePermissionModel>();
            List<PagePermissionModel> ppmView = new List<PagePermissionModel>();
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_role_permission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@role_id", SqlDbType.Int).Value = roleId;
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 123456);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            message = (string)command.Parameters["@message"].Value;
                            while (reader.Read())
                            {
                                PagePermissionModel pp = new PagePermissionModel();
                                pp.Page = Convert.ToString(reader["edit_pages"]);
                                pp.roleId = Convert.ToInt32(reader["role_id"]);
                                ppmEdit.Add(pp);
                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    PagePermissionModel pp = new PagePermissionModel();
                                    pp.Page = Convert.ToString(reader["edit_pages"]);
                                    pp.roleId = Convert.ToInt32(reader["role_id"]);
                                    ppmView.Add(pp);
                                }
                            }
                            p.pagePermissionEdit = ppmEdit;
                            p.pagePermissionView = ppmView;
                            p.message = message;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return p;
        }

        public async Task<ConfigurationModel> GetConfiguration()
        {
            ConfigurationModel pp = new ConfigurationModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_configuration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                pp.referal_wallet_balance_deduct_amount = Convert.ToDouble(reader["referal_wallet_balance_deduct_amount"]);
                                pp.down_side_direct_numer_of_joinee = Convert.ToInt32(reader["down_side_direct_numer_of_joinee"]);
                                pp.down_side_direct_of_joinee_point = Convert.ToInt32(reader["down_side_direct_of_joinee_point"]);
                                pp.point_unit_price = Convert.ToDouble(reader["point_unit_price"]);
                                pp.first_registration_wallet_balance = Convert.ToDouble(reader["first_registration_wallet_balance"]);
                                pp.wallet_approver_role = Convert.ToInt32(reader["wallet_approver_role"]);
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
            return pp;
        }

        public async Task<string> UpdateConfiguration(ConfigurationModel config)
        {
            string message = string.Empty;
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("update_configuration", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@referal_wallet_balance_deduct_amount", SqlDbType.Decimal).Value = config.referal_wallet_balance_deduct_amount;
                    command.Parameters.Add("@down_side_direct_numer_of_joinee", SqlDbType.Int).Value = config.down_side_direct_numer_of_joinee;
                    command.Parameters.Add("@down_side_direct_of_joinee_point", SqlDbType.Int).Value = config.down_side_direct_of_joinee_point;
                    command.Parameters.Add("@point_unit_price", SqlDbType.Decimal).Value = config.point_unit_price;
                    command.Parameters.Add("@first_registration_wallet_balance", SqlDbType.Decimal).Value = config.first_registration_wallet_balance;
                    command.Parameters.Add("@wallet_approver_role", SqlDbType.Int).Value = config.wallet_approver_role;
                    command.Parameters.Add("@message", SqlDbType.NVarChar, 123456);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public async Task<AddWalletResponse> AddWallet(string user_security_stamp)
        {
            AddWalletResponse response = new AddWalletResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_wallet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_security_stamp", SqlDbType.NVarChar).Value = user_security_stamp;

                    command.Parameters.Add("@message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@message"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@user_id", SqlDbType.Int, 123);
                    command.Parameters["@user_id"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@AMOUNT_WALLET_FOR_REGISTRATION", SqlDbType.Decimal, 123);
                    command.Parameters["@AMOUNT_WALLET_FOR_REGISTRATION"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            response.message = (string)command.Parameters["@message"].Value;
                            response.user_id = (Int32)command.Parameters["@user_id"].Value;
                            response.amount_wallet_for_registration = (decimal)command.Parameters["@AMOUNT_WALLET_FOR_REGISTRATION"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return response;
        }

        public async Task<AddWalletTrnsactionResponse> AddWalletTransaction(double amount, int userId, string message, string transaction_mode)
        {
            AddWalletTrnsactionResponse addWalletTrnsactionResponse = new AddWalletTrnsactionResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_wallet_transaction", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@AMOUNT_WALLET_FOR_REGISTRATION", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    command.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;
                    command.Parameters.Add("@transaction_mode", SqlDbType.NVarChar).Value = transaction_mode;
                    command.Parameters.Add("@returnMessage", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            addWalletTrnsactionResponse.message = (string)command.Parameters["@returnMessage"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return addWalletTrnsactionResponse;
        }

        public async Task<WalletWidthdrawalResponse> AddWalletWidthdrawalRequest(int request_initiator_id, string comment)
        {
            WalletWidthdrawalResponse walletWidthdrawalResponse = new WalletWidthdrawalResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_withdrawal_request", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@request_initiator_id", SqlDbType.Int).Value = request_initiator_id;
                    command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = comment;
                    command.Parameters.Add("@widthdrawal_amount", SqlDbType.Decimal, 123);
                    command.Parameters["@widthdrawal_amount"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@return_message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@return_message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            walletWidthdrawalResponse.amount_wallet_widthdraw = (decimal)command.Parameters["@widthdrawal_amount"].Value;
                            walletWidthdrawalResponse.message = (string)command.Parameters["@return_message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return walletWidthdrawalResponse;
        }

        public async Task<BalanceRequestResponse> AddWalletBalanceRequest(int request_initiator_id, decimal amount, string comment)
        {
            BalanceRequestResponse walletWidthdrawalResponse = new BalanceRequestResponse();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("add_balance_request", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@request_initiator_id", SqlDbType.Int).Value = request_initiator_id;
                    command.Parameters.Add("@comment", SqlDbType.NVarChar).Value = comment;
                    command.Parameters.Add("@amount", SqlDbType.Decimal).Value = amount;
                    command.Parameters.Add("@widthdrawal_amount", SqlDbType.Decimal, 123);
                    command.Parameters["@widthdrawal_amount"].Direction = ParameterDirection.Output;
                    command.Parameters.Add("@return_message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@return_message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            walletWidthdrawalResponse.amount_requested = (decimal)command.Parameters["@widthdrawal_amount"].Value;
                            walletWidthdrawalResponse.message = (string)command.Parameters["@return_message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return walletWidthdrawalResponse;
        }

        public async Task<AdminApprovalNotificationModel> AdminApprovalNotification(int userId)
        {
            AdminApprovalNotificationModel adminApprovalNotificationModel = new AdminApprovalNotificationModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("admin_approval_notification", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            List<WithdrawalWalletModel> withdrawals = new List<WithdrawalWalletModel>();
                            while (reader.Read())
                            {
                                adminApprovalNotificationModel.withdrawalRequestCount = Convert.ToInt32(reader["withdrawal_request_count"]);
                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    WithdrawalWalletModel pp = new WithdrawalWalletModel();
                                    pp.withdrawalid = Convert.ToInt32(reader["withdrawal_id"]);
                                    pp.firstname = Convert.ToString(reader["first_name"]);
                                    pp.middlename = Convert.ToString(reader["middle_name"]);
                                    pp.lastname = Convert.ToString(reader["last_name"]);
                                    pp.comment = Convert.ToString(reader["comment"]);
                                    pp.createdon = Convert.ToDateTime(reader["created_on"]);
                                    pp.request_initiator_id = Convert.ToInt32(reader["request_initiator_id"]);
                                    pp.wallet_balance = Convert.ToDecimal(reader["wallet_balance"]);
                                    withdrawals.Add(pp);
                                }
                            }
                            adminApprovalNotificationModel.withdrawalWalletModels = withdrawals;
                            adminApprovalNotificationModel.message = "success";
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return adminApprovalNotificationModel;
        }

        public async Task<AdminWalletAddApprovalNotificationModel> AdminAddWalletApprovalNotification(int userId)
        {
            AdminWalletAddApprovalNotificationModel adminApprovalNotificationModel = new AdminWalletAddApprovalNotificationModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("admin_wallet_add_approval_notification", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    try
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            List<AddWallet> withdrawals = new List<AddWallet>();
                            while (reader.Read())
                            {
                                adminApprovalNotificationModel.addRequestCount = Convert.ToInt32(reader["withdrawal_request_count"]);
                            };
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    AddWallet pp = new AddWallet();
                                    pp.addwalletid = Convert.ToInt32(reader["balance_request_id"]);
                                    pp.firstname = Convert.ToString(reader["first_name"]);
                                    pp.middlename = Convert.ToString(reader["middle_name"]);
                                    pp.lastname = Convert.ToString(reader["last_name"]);
                                    pp.comment = Convert.ToString(reader["comment"]);
                                    pp.createdon = Convert.ToDateTime(reader["created_on"]);
                                    pp.request_initiator_id = Convert.ToInt32(reader["request_initiator_id"]);
                                    pp.wallet_balance = Convert.ToDecimal(reader["amount"]);
                                    withdrawals.Add(pp);
                                }
                            }
                            adminApprovalNotificationModel.addWalletModels = withdrawals;
                            adminApprovalNotificationModel.message = "success";
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }

                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return adminApprovalNotificationModel;
        }

        public async Task<bool> IsWithdrawalRequestSendByThisUser(int userId)
        {
            bool isSend = false;
            AdminApprovalNotificationModel adminApprovalNotificationModel = new AdminApprovalNotificationModel();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_withdrawal_request_finder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                    try
                    {
                        int tt = (int) await command.ExecuteScalarAsync();
                        if (tt == 1)
                        {
                            isSend = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isSend;
        }

        public async Task<WithdrawalWallet> UpdateWalletWithdrawal(WithdrawalWallet withdrawalWalletModel)
        {
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_withdrawal_update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@withdrawal_id", SqlDbType.Int).Value = withdrawalWalletModel.withdrawalid;
                    command.Parameters.Add("@is_approve", SqlDbType.Bit).Value = withdrawalWalletModel.approved;
                    command.Parameters.Add("@is_rejected", SqlDbType.Bit).Value = withdrawalWalletModel.rejected;
                    command.Parameters.Add("@approver_comment", SqlDbType.NVarChar).Value = withdrawalWalletModel.admincomment;
                    command.Parameters.Add("@return_message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@return_message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            var message = (string)command.Parameters["@return_message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return withdrawalWalletModel;
        }

        public async Task<AddWallet> UpdateWalletAdd(AddWallet withdrawalWalletModel)
        {
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("wallet_add_update", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@balance_request_id", SqlDbType.Int).Value = withdrawalWalletModel.addwalletid;
                    command.Parameters.Add("@is_approve", SqlDbType.Bit).Value = withdrawalWalletModel.approved;
                    command.Parameters.Add("@is_rejected", SqlDbType.Bit).Value = withdrawalWalletModel.rejected;
                    command.Parameters.Add("@approver_comment", SqlDbType.NVarChar).Value = withdrawalWalletModel.admin_comment;
                    command.Parameters.Add("@return_message", SqlDbType.NVarChar, 1233232);
                    command.Parameters["@return_message"].Direction = ParameterDirection.Output;
                    try
                    {
                        await command.ExecuteNonQueryAsync();
                        {
                            var message = (string)command.Parameters["@return_message"].Value;
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return withdrawalWalletModel;
        }
    }
}
