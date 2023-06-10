using FilmHive.Models;
using FilmHive.Utills.FilmHive.Utils;
using Microsoft.Data.SqlClient;

namespace FilmHive.Repositories
{
    public class UserMovieRepository : BaseRepository, IUserMovieRepository
    {
        public UserMovieRepository(IConfiguration configuration) : base(configuration) { }


        public List<UserMovie> GetUserMovies()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select  Id
                                                ,userId
                                                ,movieId
                                                from [userMovie]";
                    var reader = cmd.ExecuteReader();

                    var userMovie = new List<UserMovie>();
                    while (reader.Read())
                    {
                        userMovie.Add(new UserMovie()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            movieId = DbUtils.GetInt(reader, "movieId"),
                            userId = DbUtils.GetInt(reader, "userId"),
                        });
                    }
                    reader.Close();
                    return userMovie;

                }
            }
        }






        public void AddUserMovie(UserMovie userMovie)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    Insert into userMovie ( movieId, userId)
                    output inserted.Id
                    Values (@movieId,@UserId)";


                    DbUtils.AddParameter(cmd, "@movieId", userMovie.movieId);
                    DbUtils.AddParameter(cmd, "@userId", userMovie.userId);


                    userMovie.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateUserMovie(UserMovie userMovie)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE [UserMovie]
                           SET Type = @movieId, @userId  
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@userId", userMovie.userId);
                    DbUtils.AddParameter(cmd, "@movieId", userMovie.movieId);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserMovie(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [userMovie] WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }   

    }
}
