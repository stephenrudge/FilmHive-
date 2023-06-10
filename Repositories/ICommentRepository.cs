using FilmHive.Models;

namespace FilmHive.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void DeleteComment(int id);
        List<Comment> GetCommentsByUserByMovie(int movieId, int userId);
        void UpdateComment(Comment comment);
    }
}