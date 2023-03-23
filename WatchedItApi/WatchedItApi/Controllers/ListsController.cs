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
        public async Task<IEnumerable<User>> GetAllMovieListsByUserId(int id)
            => await _context.Users
            .Include(l => l.MovieList)
            .ThenInclude(m => m.Movies)
            .Include(l => l.MovieList)
            .ThenInclude(u => u.Users)
            .Where(u => u.Id == id).ToListAsync();


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieListById(int id)
        {
            MovieList? movielist = await _context.MovieLists
                .Include(m => m.Movies)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(l => l.Id == id);

            return movielist == null ? NotFound() : Ok(movielist);
        }

        [HttpPost("addmovielist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMovieList(MovieList movielist)
        {
            if (movielist != null)
            {
                User? user = await _context.Users
                    .FindAsync(movielist.UserId);

                if(user != null)
                {
                    await _context.MovieLists.AddAsync(movielist);
                    await _context.SaveChangesAsync();

                    MovieListDto dto = new MovieListDto
                    {
                        MovieListId = movielist.Id,
                        phoneNumber = user.Phone,
                    };

                    await AddUserToList(dto);
                    await _context.SaveChangesAsync();

                    return Ok("created");
                }
                
                return BadRequest("user not found");
            }

            return BadRequest("no info found");
        }

        [HttpPost("addmovietolist")]
        [ProducesResponseType(typeof(MovieListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddMovieToList([FromForm] MovieListDto request)
        {
            var mymovielist = await _context.MovieLists
                .FindAsync(request.MovieListId);

            if (mymovielist != null)
            {
                var newMovie = new Movie
                {
                    MovieListId = request.MovieListId,
                    ExternId = request.externId
                };

                await _context.Movies.AddAsync(newMovie);
                await _context.SaveChangesAsync();

                return Ok("movie added");
            }

            return BadRequest("no info found");
        }

        [HttpDelete("removemoviefromlist")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveMovieFromList(int movieId)
        {
            var movie = await _context.Movies
                .FindAsync(movieId);

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
        public async Task<IActionResult> RemoveMovieList(int movielistid)
        {
            var movielist = await _context.MovieLists
                .Include(m => m.Movies)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(l => l.Id == movielistid);

            if (movielist != null)
            {
                if (movielist.Movies != null)
                {
                    foreach (var movie in movielist.Movies)
                    {
                        await RemoveMovieFromList(movie.Id);
                    }
                }

                _context.MovieLists.Remove(movielist);
                await _context.SaveChangesAsync();

                return Ok("removed");
            }

            return BadRequest("no info found");
        }

        [HttpPost("addusertolist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUserToList([FromForm] MovieListDto request)
        {
            MovieList? mymovielist = await _context.MovieLists
                .Include(u => u.Users)
                .FirstOrDefaultAsync(l => l.Id == request.MovieListId);

            if (mymovielist != null)
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == request.phoneNumber);

                if (user != null)
                {
                    mymovielist.Users.Add(user);

                    await _context.SaveChangesAsync();

                    return Ok("user added");
                }

                return BadRequest("user not found");
            }

            return BadRequest("no info found");
        }

        [HttpDelete("removeuserfromlist")]
        [ProducesResponseType(typeof(MovieList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveUserFromList([FromForm] MovieListDto request)
        {
            MovieList? mymovielist = await _context.MovieLists
                .Include(u => u.Users)
                .FirstOrDefaultAsync(l => l.Id == request.MovieListId);

            if (mymovielist != null)
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == request.phoneNumber);

                if (user != null)
                {
                    mymovielist.Users.Remove(user);

                    await _context.SaveChangesAsync();

                    return Ok("user removed");
                }

                return BadRequest("user not found");
            }

            return BadRequest("no info found");
        }

        [HttpGet("test123")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieListByUserId(int id)
        {
            User? user = await _context.Users
                .Include(m => m.MovieList)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user == null ? NotFound() : Ok(user);
        }
    }
}
