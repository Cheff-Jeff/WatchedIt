﻿using Microsoft.AspNetCore.Http;
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
                Friend friend = new Friend(){ Id = 0, Name = user.Name, MovieList = null, phoneNumber = user.Phone, UserId = (int)dto.Id };
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
            //return await _context.Friends.Where(u => u.UserId == dto.Id).ToListAsync();
            //IQueryable<Friend> friends = _context.Friends;
            //friends = friends.Where(f => f.u == dto.Id);
            //return Ok(await friends.ToArrayAsync());
        }
    }
}
