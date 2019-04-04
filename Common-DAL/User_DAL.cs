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
                            var user = new User
                            {
                                user_id = Convert.ToInt32(reader["user_id"]),
                                user_name = Convert.ToString(reader["user_name"]),
                                email = Convert.ToString(reader["email"]),
                                security_stamp = Convert.ToString(reader["security_stamp"]),
                                role_name = Convert.ToString(reader["role_name"]),
                                first_name = Convert.ToString(reader["first_name"]),
                                last_name = Convert.ToString(reader["last_name"]),
                                father_name = Convert.ToString(reader["father_name"]),
                                dob = Convert.ToDateTime(reader["dob"]).ToString("dd/MM/yyyy"),
                                mobile_number = Convert.ToString(reader["mobile_number"]),
                                pan_card = Convert.ToString(reader["pan_card"]),
                                aadhar_card = Convert.ToString(reader["aadhar_card"]),
                                address = Convert.ToString(reader["address"]),
                                post_office = Convert.ToString(reader["post_office"]),
                                police_station = Convert.ToString(reader["police_station"]),
                                district = Convert.ToString(reader["district"]),
                                city = Convert.ToString(reader["city"]),
                                state_name = Convert.ToString(reader["state_name"]),
                                pin = Convert.ToString(reader["pin"]),
                                sex = Convert.ToString(reader["sex"])
                            };
                            results.Add(user);
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
                        };

                    }
                }
            }

            Console.WriteLine("All done. Press any key to finish...");
            return result;
        }

        public async Task<bool> Find_Users(string user_name, string password)
        {
            bool isUserExist = false;
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


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            int i = Convert.ToInt32(reader[0]);
                            if (i > 0)
                            {
                                isUserExist = true;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isUserExist;
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


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            referelToken = Convert.ToString(reader[0]);

                        }
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return referelToken;
        }

        public async Task<bool> ValidateReferelToken(string token)
        {
            bool isValid = false;
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

                    await command.ExecuteReaderAsync();
                    {
                        isValid = (bool)command.Parameters["@bit"].Value;
                    }
                }
            }
            Console.WriteLine("All done. Press any key to finish...");
            return isValid;
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
    }
}
