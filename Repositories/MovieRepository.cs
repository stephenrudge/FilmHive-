using FilmHive.Models;
using FilmHive.Utills.FilmHive.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace FilmHive.Repositories
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(IConfiguration configuration) : base(configuration) { }

        public List<Movie> GetMovies()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select  Id
                                                ,title
                                                ,poster_path
                                                ,overview
                                                from [Movies]";
                    var reader = cmd.ExecuteReader();

                    var movies = new List<Movie>();
                    while (reader.Read())
                    {
                        movies.Add(new Movie()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Title = DbUtils.GetString(reader, "title"),
                            PosterPath = DbUtils.GetString(reader, "poster_path"),
                            Overview = DbUtils.GetString(reader, "overview"),
                        });
                    }
                    reader.Close();
                    return movies;

                }
            }
        }



        public List<MovieForWatchlist> GetWatchlistByUser(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.id as MovieId, title, poster_path, overview, UM.id as UserMovieId
                                        from userMovie UM
                                        join Movies M on m.id = UM.movieId
                                        Where UM.userId = @userId";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    var reader = cmd.ExecuteReader();

                    var movies = new List<MovieForWatchlist>();
                    while (reader.Read())
                    {
                        movies.Add(new MovieForWatchlist()
                        {
                            Id = DbUtils.GetInt(reader, "MovieId"),
                            Title = DbUtils.GetString(reader, "title"),
                            PosterPath = DbUtils.GetString(reader, "poster_path"),
                            UserMovieId = DbUtils.GetInt(reader, "UserMovieId"),

                        });
                    }
                    reader.Close();
                    return movies;

                }
            }
        }

        public UserMovie PostWatchlistByUser(UserMovie userMovie)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert into [userMovie] (movieId, userId)
                                        OUTPUT Inserted.Id

                                        VALUES(@movieId, @userId)";
                    DbUtils.AddParameter(cmd, "@movieId", userMovie.movieId);
                    DbUtils.AddParameter(cmd, "@userId", userMovie.userId);


                    userMovie.Id = (int)cmd.ExecuteScalar();
                }
                return userMovie;
            }
        }


        public List<Movie> GetMovieWithUsersComments(int userId, int movieId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT MC.Id AS 'CommentId', m.id as 'MovieId', title, poster_path, [text], MC.movieId, UM.userId
                    FROM Movies M 
					left join userMovie UM
                    ON M.id = UM.movieId
                     left JOIN MComments MC ON MC.movieId = M.id
                    WHERE UM.userId = @userId and M.id = @movieId";

                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    var reader = cmd.ExecuteReader();
                    Movie? movie = null;
                    var movies = new List<Movie>();
                    var comments = new List<Comment>();
                    while (reader.Read())
                    {
                        var currentMovieId = DbUtils.GetInt(reader, "MovieId");
                        if (movie == null || movie.Id != currentMovieId)
                        {
                            if (movie != null)
                            {
                                movies.Add(movie);
                            }
                            movie = new Movie()

                            {
                                Id = DbUtils.GetInt(reader, "MovieId"),
                                Title = DbUtils.GetString(reader, "title"),
                                PosterPath = DbUtils.GetString(reader, "poster_path"),


                            };
                        }
                        // if (DbUtils.IsNotDbNull(reader, ""))

                        if (DbUtils.IsNotDbNull(reader, "CommentId"))
                        {
                            movie.Comments.Add(new Comment()

                            {
                                Id = DbUtils.GetInt(reader, "CommentId"),
                                Text = DbUtils.GetString(reader, "text"),
                                MovieId = DbUtils.GetInt(reader, "movieId"),
                                UserId = DbUtils.GetInt(reader, "userId"),

                            });
                        };
                    }
                    if (movie != null)
                    {
                        movies.Add(movie);
                    }
                    reader.Close();
                    return movies;
                }

            }
        }



        public Movie GetMovieById(int Id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  Id
                                                ,title
                                                ,poster_path
                                                ,overview
                                                from [Movies]
												WHERE id =@id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Movie movie = null;

                    if (reader.Read())
                    {
                        movie = new Movie()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Title = DbUtils.GetString(reader, "title"),
                            PosterPath = DbUtils.GetString(reader, "poster_path"),
                            Overview = DbUtils.GetString(reader, "overview"),
                        };
                    }
                    reader.Close();
                    return movie;

                }
            }
        }


        public List<Movie> GetMovieBySearch(string title)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT  Id
                                                ,title
                                                ,poster_path
                                                ,overview
                                                from [Movies]
												WHERE title like '%'+ @title + '%'";
                    cmd.Parameters.AddWithValue("@title", title);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Movie> movies = new List<Movie>();

                    while (reader.Read())
                    {
                        Movie movie = new Movie()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            Title = DbUtils.GetString(reader, "title"),
                            PosterPath = DbUtils.GetString(reader, "poster_path"),
                            Overview = DbUtils.GetString(reader, "overview"),
                        };
                        movies.Add(movie);
                    }
                    reader.Close();
                    return movies;

                }
            }
        }


        public void AddMovie(Movie movie)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Movies] (Title, poster_path, Overview )
                    OUTPUT INSERTED.Id
                    VALUES (@Title,@poster_path, @overview)";


                    DbUtils.AddParameter(cmd, "@title", movie.Title);
                    DbUtils.AddParameter(cmd, "@poster_path", movie.PosterPath);
                    DbUtils.AddParameter(cmd, "@overview", movie.Overview);

                    movie.Id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public void UpdateMovie(Movie movie)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE [Movies]
                           SET Type = @Title, @poster_path, @Overview,  
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", movie.Title);
                    DbUtils.AddParameter(cmd, "@poster_path", movie.PosterPath);
                    DbUtils.AddParameter(cmd, "@Overview", movie.Overview);


                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void DeleteMovie(int movieId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {   
                    cmd.CommandText = "DELETE FROM [userMovies] WHERE MovieId = @MovieId";
                    DbUtils.AddParameter(cmd, "@movieId", movieId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
