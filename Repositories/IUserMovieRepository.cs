using FilmHive.Models;

namespace FilmHive.Repositories
{
    public interface IUserMovieRepository
    {
        void AddUserMovie(UserMovie userMovie);
        void DeleteUserMovie(int id);
        List<UserMovie> GetUserMovies();
        void UpdateUserMovie(UserMovie userMovie);
    }
}