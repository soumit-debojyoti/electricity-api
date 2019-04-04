using Electricity_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Electricity_DAL
{
    public class Employee_DAL
    {
        private string _connectionString = string.Empty;
        public Employee_DAL(ConnectionStrings connectionString)
        {
            this._connectionString = connectionString.myConnectionString;
        }

        public async Task<List<Employee>> Get_Employees()
        {
            List<Employee> results = new List<Employee>();
            Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");
            Console.Write("Connecting to SQL Server ... ");
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();
                Console.WriteLine("Done.");
                using (SqlCommand command = new SqlCommand("get_employees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                id = Convert.ToInt32(reader["id"]),
                                name = Convert.ToString(reader["name"]),
                                address = Convert.ToString(reader["address"])
                            };
                            results.Add(employee);
                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return results;
        }
    }
}
