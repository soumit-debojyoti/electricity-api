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
    }
}
