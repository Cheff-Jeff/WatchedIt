using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchedItApi.Data;
using WatchedItApi.Models;

namespace WatchedItApi.Controllers
{
    [Route("api/[controller]")]
    public class ListsController : Controller
    {
        private readonly Context _context;
        public ListsController(Context context) => _context = context;


        [HttpGet]
        public async Task<IEnumerable<MovieList>> GetAllByUserId(int id)
            => await _context.MovieLists.Include(m => m.Movies).Where(u => u.userId == id).ToListAsync();


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieListById(int id)
        {
            MovieList? movielist = await _context.MovieLists.Include(m => m.Movies).FirstOrDefaultAsync(l => l.id == id);

            return movielist == null ? NotFound() : Ok(movielist);
        }

        [HttpPost("addmovielist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMovieList(MovieList movielist)
        {
            if (movielist != null)
            {
                await _context.MovieLists.AddAsync(movielist);
                await _context.SaveChangesAsync();

                return Ok("created");
            }

            return BadRequest("no info found");
        }

        [HttpPost("addmovietolist")]
        [ProducesResponseType(typeof(MovieListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMovieToList([FromForm] MovieListDto request)
        {
            var mymovielist = await _context.MovieLists.FindAsync(request.movieListId);
            if (mymovielist != null)
            {
                var newMovie = new Movie
                {
                    movieListId = request.movieListId,
                    ExternId = request.externId
                };

                await _context.Movies.AddAsync(newMovie);
                await _context.SaveChangesAsync();

                return Ok("movie added");
            }

            return BadRequest("no info found");
        }

        [HttpDelete("removemoviefromlist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveMovieFromList(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();

                return Ok("removed");
            }

            return BadRequest("no info found");
        }

        [HttpDelete("removemovielist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveMovieList(int id)
        {
            var movielist = await _context.MovieLists.FindAsync(id);
            if (movielist != null)
            {
                _context.MovieLists.Remove(movielist);
                await _context.SaveChangesAsync();

                return Ok("removed");
            }

            return BadRequest("no info found");
        }
    }
}
