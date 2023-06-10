namespace FilmHive.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public List<Genre> Genres { get; set; }
        public string PosterPath { get; set; }

        public string Overview { get; set; }
        public List<Comment> Comments { get; set; } = new();
     

    }

    public class MovieForWatchlist : Movie 
    {
        public int UserMovieId { get; set; }
    }
}
    