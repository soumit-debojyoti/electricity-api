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

        public async Task<string> AddWallet(string user_security_stamp)
        {
            string message = string.Empty;
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
    }
}
