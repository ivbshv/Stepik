using MySql.Data.MySqlClient;
using Stepik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Stepik.Services
{
    public class CommentsService
    {
        public static List<Comment> Get(int id)
        {
            var comments = new List<Comment>();

            using var connections = new MySqlConnection(Constant.ConnectionString);

            connections.Open();

            var query = @"SELECT c.id, c.text, c.time
            FROM comments AS c
            JOIN steps AS s ON c.step_id = s.id
            JOIN unit_lessons AS ul ON s.id = ul.lesson_id
            JOIN lessons AS l ON ul.lesson_id = l.id
            JOIN units AS u ON ul.unit_id = u.id
            JOIN courses AS cr ON u.course_id = cr.id
            WHERE reply_comment_id IS NULL AND cr.id = @id
            ORDER BY c.time DESC;";

            using var command = new MySqlCommand(query, connections);

            var Params = new MySqlParameter("@id", id);

            command.Parameters.Add(Params);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var comment = new Comment()
                {
                    Id = reader.GetInt32("id"),
                    Text = reader.GetString("text"),
                    Time = reader.GetDateTime("time")
                };

                comments.Add(comment);

            }

            return comments;
        }

        public static bool Delete(int id)
        {
            using var connection = new MySqlConnection(Constant.ConnectionString);
            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                string query = @"DELETE FROM course_reviews
                                WHERE comment_id = @id;";

                using var command = new MySqlCommand(query, connection, transaction);

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                command.CommandText = $@"DELETE FROM comments
                                     WHERE reply_comment_id = @id;";

                command.ExecuteNonQuery();

                command.CommandText = @"DELETE FROM comments
                                     WHERE id = @id;";

                command.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
