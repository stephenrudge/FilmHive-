using FilmHive.Models;
using FilmHive.Utills.FilmHive.Utils;
using Microsoft.Data.SqlClient;

namespace FilmHive.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }
                    
        public List<Comment> GetCommentsByUserByMovie(int movieId, int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select [text], userId, movieId,id
                                        From [MComments] Where movieId = @movieId and userId=@userId";
                    DbUtils.AddParameter(cmd, "@movieId", movieId);
                    DbUtils.AddParameter(cmd, "@userId", userId);
                    var reader = cmd.ExecuteReader();
                    var comment = new List<Comment>();
                    while (reader.Read())
                    {
                        comment.Add(new Comment()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "userId"),
                            Text = DbUtils.GetString(reader, "text"),
                            MovieId = DbUtils.GetInt(reader, "movieId"),
                        });
                    }
                    reader.Close();
                    return comment;

                }
            }
        }


        public void AddComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [MComments] (movieId , userId, text)
                    OUTPUT INSERTED.Id
                    VALUES ( @movieId, @userId, @text)";

                    DbUtils.AddParameter(cmd, "@movieId", comment.MovieId);
                    DbUtils.AddParameter(cmd, "@userId", comment.UserId);
                    DbUtils.AddParameter(cmd, "@text", comment.Text);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE [MComments]
                           SET text = @text
                         WHERE id =@Id";
             
                    DbUtils.AddParameter(cmd, "text", comment.Text);
                    DbUtils.AddParameter(cmd, "id", comment.Id);

                    cmd.ExecuteNonQuery();
                }
            }       
        }


        public void DeleteComment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [MComments] WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
