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
    }
}
