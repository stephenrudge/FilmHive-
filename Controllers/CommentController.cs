using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FilmHive.Models;
using FilmHive.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FilmHive.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get(int userId, int movieId)
            {
            return Ok(_commentRepository.GetCommentsByUserByMovie(movieId, userId));
        }

        // GET api/<UsersController>/5



        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post(Comment comment)
        {
            _commentRepository.AddComment(comment);
            return CreatedAtAction("Get", new { id = comment.Id }, comment);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, Comment comment)
        {
          
            _commentRepository.UpdateComment(comment);
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _commentRepository.DeleteComment(id);
            return NoContent();
        }
    }
}
