using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

        public static User Get(string fullName)
        {
            User user = new User();

            using var connection = new MySqlConnection(Constant.ConnectionString);

            connection.Open();

            string query = @"SELECT FullName, Details, JoinDate, Avatar, IsActive 
                        FROM users 
                        WHERE FullName = @FullName AND IsActive = 1";

            using MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@FullName", fullName);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {

                user.FullName = reader.IsDBNull(0) ? null : reader.GetString(0);
                user.Details = reader.IsDBNull(1) ? null : reader.GetString(1);
                user.JoinDate = reader.IsDBNull(2) ? DateTime.Now : reader.GetDateTime(2);
                user.Avatar = reader.IsDBNull(3) ? null : reader.GetString(3);
                user.IsActive = reader.IsDBNull(4) ? true : reader.GetBoolean(4);

            }

            return null;
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
    }
}
