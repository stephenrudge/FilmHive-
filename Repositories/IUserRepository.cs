using FilmHive.Models;
using Microsoft.Data.SqlClient;

namespace FilmHive.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        User? GetUserByEmail(string email);
        List<User> GetUsers();
        void Update(User user);
    }
}