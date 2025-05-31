using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Models.Services
{
    public class UsersService
    {
        public static bool Add(User user)
        {
            try
            {
                string connections = Constant.ConnectionString;

                using var connection = new MySqlConnection(connections);

                connection.Open();

                string sqlQuery = @"INSERT INTO users 
                                    (FullName, Details, JoinDate, Avatar, IsActive) 
                                    VALUES 
                                    (@FullName, @Details, @JoinDate, @Avatar, @IsActive)";

                using var command = new MySqlCommand(sqlQuery, connection);

                var execute = command.ExecuteNonQuery();

                command.Parameters.AddWithValue("@FullName", user.FullName);
                command.Parameters.AddWithValue("@Details", user.Details);
                command.Parameters.AddWithValue("@JoinDate", user.JoinDate);
                command.Parameters.AddWithValue("@Avatar", user.Avatar);
                command.Parameters.AddWithValue("@IsActive", user.IsActive);

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch
            {
                return false;
            }
        }

        public static User? Get(string fullName)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();
            var query = @"SELECT * FROM users
                   WHERE full_name = @FullName AND is_active = 1;";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", fullName);
            using var reader = command.ExecuteReader();
            return reader.Read()
                ? new User
                {
                    FullName = reader.GetString("full_name"),
                    Details = reader.IsDBNull("details") ? null : reader.GetString("details"),
                    JoinDate = reader.GetDateTime("join_date"),
                    Avatar = reader.IsDBNull("avatar") ? null : reader.GetString("avatar"),
                    IsActive = reader.GetBoolean("is_active"),
                    Knowledge = reader.GetInt32("knowledge"),
                    Reputation = reader.GetInt32("reputation"),
                    FollowersCount = reader.GetInt32("followers_count")
                }
                : null;
        }


        public static void LoginUser()
        {
            Console.WriteLine("Введите имя и фамилию через пробел и нажмите Enter:");

            string userName = Console.ReadLine();

            var user = UsersService.Get(userName);

            if (user != null)
            {
                Console.WriteLine($"Пользователь '{user.FullName}' успешно вошел\n");
            }
            else
            {
                Console.WriteLine("Пользователь не найден, произведен выход на главную страницу\n");
            }

        }

        public static int GetTotalCount()
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);

            connection.Open();

            string query = @"SELECT COUNT(*) FROM users";

            using var command = new MySqlCommand(query, connection);

            int usersCount = (int)command.ExecuteScalar();

            return usersCount;

        }

        public static string FormatUserMetrics(int number)
        {
            var connections = Constant.ConnectionString;

            using var connection = new MySqlConnection(connections);

            connection.Open();

            var storedProcedureName = "format_number";
            using var command = new MySqlCommand(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            var numberParam = new MySqlParameter("number", number)
            {
                Direction = ParameterDirection.Input,
            };

            command.Parameters.Add(numberParam);

            var returnParam = new MySqlParameter()
            {
                Direction = ParameterDirection.ReturnValue
            };

            command.Parameters.Add(returnParam);

            command.ExecuteNonQuery();

            var returnValue = returnParam.Value;

            return returnValue != null ? returnValue.ToString() : string.Empty;
        }
    }
}
