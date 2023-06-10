using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FilmHive.Models;
using FilmHive.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmHive.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserMovieController : ControllerBase
    {
        private readonly IUserMovieRepository _userMovieRepository;
        public UserMovieController(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        [HttpGet]
        public IActionResult GetUserMovies()
        {
            return Ok(_userMovieRepository.GetUserMovies());
        }

        [HttpPost]
        public IActionResult Post(UserMovie userMovie)
        {
            _userMovieRepository.AddUserMovie(userMovie);
            return Ok(userMovie);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, UserMovie userMovie)
        {
            if (id != userMovie.Id)
            {
                return BadRequest();
            }
            _userMovieRepository.UpdateUserMovie(userMovie);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userMovieRepository.DeleteUserMovie(id);
            return NoContent();
        }
    }
}
