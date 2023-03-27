using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchedItApi.Data;
using WatchedItApi.Models;

namespace WatchedItApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        public UsersController(Context context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
            => await _context.Users.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(UserDto dto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user != null)
            {
                if (dto.Email != null && dto.Password != null)
                {
                    EncryptionHandler handler = new EncryptionHandler();
                    if (handler.VerifyPassword(user.Password, user.Salt, dto.Password))
                    {
                        return Ok(user);
                    }
                    return BadRequest("Bad login information");
                }
                return BadRequest("Bad login information not complete");
            }
            return BadRequest("User not found");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] UserDto dto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
            {
                if (dto.Name != null && dto.Email != null && dto.Password != null)
                {
                    User newUser = new User(dto);
                    newUser.encrypt();
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
                }
                return BadRequest("Information not complete");
            }
            return BadRequest("User already exists");
        }

        [HttpPost]
        [Route("friend")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFriend(UserDto dto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Phone == dto.Phone);
            if (user != null && dto.Id != null)
            {
                Friend friend = new Friend() { Id = 0, Name = user.Name, phoneNumber = user.Phone, UserId = (int)dto.Id };
                await _context.Friends.AddAsync(friend);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetFriends), new { id = friend.Id }, friend);

            }
            return BadRequest("Your friend does not have an account.");
        }

        [HttpGet]
        [Route("friend")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFriends(int userId)
        {
            if (!String.IsNullOrEmpty(userId.ToString()))
            {
                IQueryable<Friend> friends = _context.Friends;
                friends = friends
                    .Where(u => u.UserId == userId);

                return Ok(await friends.ToArrayAsync());
            }
            return BadRequest("User not found");
        }

        [HttpDelete]
        [Route("friend")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFriend(int id) 
        {
            Friend? friend  = await _context.Friends.FindAsync(id);
            if (friend == null) return BadRequest();

            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("updateuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updateUser(UserDto dto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
            if (user != null)
            {
                if (dto.Name != null && dto.Email != null)
                {
                    _context.Entry(user).State = EntityState.Modified;

                    User? newInfo = new User(dto);
                    if (dto.Password != null)
                    {
                        newInfo.encrypt();
                    }
                    else
                    {
                        newInfo.keepOldInfo(user.Salt, user.Password);
                    }

                    user.Name = newInfo.Name;
                    user.Email = newInfo.Email;
                    user.Phone = newInfo.Phone;
                    user.Password = newInfo.Password;
                    user.Salt = newInfo.Salt;

                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                return BadRequest("Information not complete");
            }
            return BadRequest("Not Found");   
        }

        [HttpGet]
        [Route("favorites")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            if (!String.IsNullOrEmpty(userId.ToString()))
            {
                IQueryable<Favorites> favorites = _context.Favorites;
                favorites = favorites
                    .Where(u => u.userId == userId);

                return Ok(await favorites.ToArrayAsync());
            }
            return BadRequest("User not found");
        }

        [HttpPost]
        [Route("favorites")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFovorite(FavoriteDto dto)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.userId);
            if (user != null && dto.movieId != null)
            {
                Favorites favorite = new Favorites() { Id = 0, userId = user.Id, movieId = (int)dto.movieId, movie = (bool)dto.movie };
                await _context.Favorites.AddAsync(favorite);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetFriends), new { id = user.Id }, user);
            }
            return BadRequest("Your friend does not have an account.");
        }

        [HttpDelete]
        [Route("favorites")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            Favorites? favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null) return BadRequest();

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("favorites/check")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> checkFavorite(FavoriteDto dto)
        {
            List<Favorites> favorites = await _context.Favorites
                .Where(u => u.userId == dto.userId)
                .ToListAsync();

            if (favorites.Count() > 0) 
            {
                for (int i = 0; i < favorites.Count(); i++)
                {
                    if (favorites[i].movieId == dto.movieId) 
                    {
                        return Ok(favorites[i]);
                    }
                }
                return BadRequest("Not a favorite");
            }

            return BadRequest("No element found");
        }
    }
}
