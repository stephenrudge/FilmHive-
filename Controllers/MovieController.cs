using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FilmHive.Models;
using FilmHive.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmHive.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(_movieRepository.GetMovies());
        }

        [HttpGet("watchlist/{userId}")]
        
        public IActionResult GetWatchlistByUser(int userId)
        {
            return Ok(_movieRepository.GetWatchlistByUser(userId));
        }

        [HttpPost("watchlist")]

        public IActionResult PostWatchlistByUser(UserMovie userMovie)
        {
            return Ok(_movieRepository.PostWatchlistByUser(userMovie));
        }


        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id, int? userId = null)
        {
            if (userId == null)
            {
            return Ok(_movieRepository.GetMovieById(id));
            }
            else
            {
                return Ok(_movieRepository.GetMovieWithUsersComments((int) userId, id));
            }


        }

        [HttpGet("/search")]
        public IActionResult GetMovieBySearch(string title)
        {
            return Ok(_movieRepository.GetMovieBySearch(title));
        }


        [HttpPost]
        public IActionResult Post(Movie movie)
        {
            _movieRepository.AddMovie(movie);
            return Ok(movie);
        }

     
        [HttpPut("{id}")]
        public IActionResult Put(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            _movieRepository.UpdateMovie(movie);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int movieId)
        {
            _movieRepository.DeleteMovie(movieId);
            return NoContent();
        }
    }
}
