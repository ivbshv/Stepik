using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stepik.Models.Services
{
    public class CoursesService
    {
        public static int GetTotalCount()
        {
            string connections = Constant.ConnectionString;

            using var connection = new  MySqlConnection(connections);

            connection.Open();

            var query = @"SELECT COUNT(*) FROM courses";

            using var command = new MySqlCommand(query, connection);

            var courseCount = command.ExecuteScalar() ;

            return courseCount != null ?  Convert.ToInt32(courseCount) : 0;
        }
    }
}
