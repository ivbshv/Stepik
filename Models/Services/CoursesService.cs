using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static List<Course> Get(string fullName)
        {
            var courses = new List<Course>();

            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            var query = @"
            SELECT title, summary, photo
            FROM user_courses
            JOIN courses ON user_courses.course_id = courses.id
            JOIN users ON users.id = user_courses.user_id
            WHERE users.full_name = @fullName AND users.is_active = 1
            ORDER BY user_courses.last_viewed DESC;";

            using var command = new MySqlCommand(query, connection);
            var fullNameParam = new MySqlParameter("@fullName", fullName);
            command.Parameters.Add(fullNameParam);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var course = new Course
                {
                    Title = reader.GetString(0),
                    Summary = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Photo = reader.IsDBNull(2) ? null : reader.GetString(2)
                };
                courses.Add(course);
            }

            return courses;
        }

    }
}
