using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_DAL
{
    public class Role_DAL
    {
        private string _connectionString = string.Empty;
        public Role_DAL(ConnectionStrings connectionString)
        {
            this._connectionString = connectionString.myConnectionString;
        }

        public async Task<List<Role>> Get_Roles()
        {
            List<Role> results = new List<Role>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_roles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var role = new Role
                            {
                                role_id = Convert.ToInt32(reader["role_id"]),
                                role_name = Convert.ToString(reader["role_name"])
                            };
                            results.Add(role);
                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return results;
        }
    }
}
