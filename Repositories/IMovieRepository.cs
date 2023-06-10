using FilmHive.Models;

namespace FilmHive.Repositories
{
    public interface IMovieRepository
    {
        void AddMovie(Movie movie);
        void DeleteMovie(int id);
        List<Movie> GetMovies();
        void UpdateMovie(Movie movie);
        Movie GetMovieById(int id);
        List<Movie> GetMovieBySearch(string title);
        List<MovieForWatchlist> GetWatchlistByUser(int userId);
        UserMovie PostWatchlistByUser(UserMovie userMovie);
        List<Movie> GetMovieWithUsersComments(int userId, int movieId);
    }
}