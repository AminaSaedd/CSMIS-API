using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CSS.Data;
using CSS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CSS.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace CSS.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersController(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> Register(UserDTO user)
        {
            // Check if a user with the same name exists in the DB first
            var userExists = _context.Users.Any(u => u.UserName.Equals(user.UserName));
            if (userExists is true)
            {
                return BadRequest(new
                {
                    message = "Username already exists."
                });
            }
                
            // Create the user object
            User newUser = new User
            {
                CreatedByID = 0,
                CreatedAt = DateTime.Now,
                UserName = user.UserName,
                Password = user.Password,
                Name = user.Name
            };

            // Hash the Password
            var hashedPassword = _passwordHasher.HashPassword(newUser, user.Password);
            newUser.Password = hashedPassword;

            // Save the User
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UserStatus(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user is null) return NotFound();

            user.IsActive = user.IsActive != true;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }
        //change A userPassword 
        [HttpPut("ChangeUserPassword/{id}")]
        public async Task<IActionResult>ResetPassword(int id,ChangePasswordDTO changePassword)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                    return BadRequest();

            var hashedPassword = _passwordHasher.HashPassword(user, changePassword.NewPassword);

            user.Password = hashedPassword;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();           
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
