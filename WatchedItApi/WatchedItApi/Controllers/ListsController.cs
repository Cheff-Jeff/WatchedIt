using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WatchedItApi.Data;
using WatchedItApi.Data.Migrations;
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
        public async Task<IActionResult> AddMovieList([FromForm] MovieList movielist)
        {
            if (movielist != null)
            {
                User? user = await _context.Users
                    .FindAsync(movielist.UserId);

                if(user != null)
                {
                    //movielist toevoegen
                    await _context.MovieLists.AddAsync(movielist);
                    await _context.SaveChangesAsync();



                    //user aan list toevoegen
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
                .Include(m => m.Movies)
                .FirstOrDefaultAsync(ml => ml.Id == request.MovieListId);

            bool doubleMovie = false;

            if (mymovielist != null && DateTime.Parse(mymovielist.addMovieDeadLine) > DateTime.Now)
            {
                if(mymovielist.Movies != null)
                {
                    doubleMovie = mymovielist.Movies.Any(m => m.ExternId == request.externId && m.movie == request.movie);
                }
                
                if (doubleMovie == false)
                {
                    var newMovie = new Movie
                    {
                        MovieListId = request.MovieListId,
                        ExternId = request.externId,
                        movie = request.movie
                    };

                    await _context.Movies.AddAsync(newMovie);
                    await _context.SaveChangesAsync();

                    return Ok("movie added");
                }
                return BadRequest("movie already in list");
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
                if(movielist.Users != null)
                {
                    foreach (var user in movielist.Users)
                    {
                        var uservote = await _context.UserVotes
                            .FirstOrDefaultAsync(uv => uv.MovieListId == movielist.Id && uv.userId == user.Id);

                        if (uservote != null)
                        {
                            _context.UserVotes.Remove(uservote);
                        }
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
                User? user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Phone == request.phoneNumber);

                if (user != null)
                {
                    mymovielist.Users.Add(user);
                    await _context.SaveChangesAsync();

                    //user aan votedlist toevoegen
                    UserVote userVote = new UserVote
                    {
                        MovieListId = mymovielist.Id,
                        userId = user.Id,
                        voted = false
                    };

                    await _context.UserVotes.AddAsync(userVote);
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
                .FirstOrDefaultAsync(ml => ml.Id == request.MovieListId);

            if (mymovielist != null)
            {
                User? user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Phone == request.phoneNumber);

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

        [HttpPost("ratemovie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVoteCountToMovie([FromForm] UserVoteDto request)
        {
            UserVote? myuserVote = await _context.UserVotes
                .FirstOrDefaultAsync(uv => uv.MovieListId == request.movielistId && uv.userId == request.userId);

            if (myuserVote != null && myuserVote.voted == false)
            {
                MovieList? mymovielist = await _context.MovieLists
                    .Include(ml => ml.Movies)
                    .FirstOrDefaultAsync(ml => ml.Id == request.movielistId);

                if(mymovielist != null && mymovielist.Movies != null && request.externMovieIds != null)
                {
                    foreach(var externMovieId in request.externMovieIds)
                    { 
                        Movie? movieitem = mymovielist.Movies
                            .FirstOrDefault(m => m.ExternId == externMovieId);

                        if(movieitem != null)
                        {
                            movieitem.voteCount += 1;
                        }
                    }
                }

                myuserVote.voted = true;

                await _context.SaveChangesAsync();

                return Ok("votes added");
            }

            return BadRequest("already voted");
        }

        [HttpGet("votecheck")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> getHasUserVoted(int movielistId, int userId)
        {
            UserVote? myuserVote = await _context.UserVotes
                .FirstOrDefaultAsync(uv => uv.MovieListId == movielistId && uv.userId == userId);

            return myuserVote == null ? NotFound() : Ok(myuserVote.voted);
        }

        [HttpGet("getmovievotesresult")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieVotesResult(int movielistId)
        {
            MovieList? mymovielist = await _context.MovieLists
                .Include(ml => ml.Movies)
                .FirstOrDefaultAsync(ml => ml.Id == movielistId);

            if (mymovielist != null && mymovielist.Movies != null)
            {
                Movie? mymovie = mymovielist.Movies.MaxBy(m => m.voteCount);

                return Ok(mymovie);
            }

            return BadRequest("already voted");
        }
    }
}
