using CSS.Data;
using CSS.DTOs;
using CSS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CSS.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Login(AuthDTO credentials)
        {
            // check if a user with this userName exists 
            var user = _context.Users.FirstOrDefault(u => u.UserName == credentials.UserName);
            if (user == null) return new UnauthorizedObjectResult(new { message = "You entered wrong credentials, please try again." });

            // check if the password is correct and get the token
            var isAuthenticated = Authenticate(credentials, user);
            if (!isAuthenticated) return new UnauthorizedObjectResult(new { message = "You entered wrong credentials, please try again." });

            // check if the user is Active
            if(user.IsActive == false) return new UnauthorizedObjectResult(new { message = "Your user is not active, please contact the management to activate your user." });

            var userInfo = new User
            {
                Id=user.Id,
                UserName= user.UserName,
                Name=user.Name
            };

            var token = GenerateToken(userInfo);
            return Ok(new { token = token });
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        private bool Authenticate(AuthDTO credentials, User user)
        {
            // hash the password first
            var hashedPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, credentials.Password);

            if (hashedPassword != PasswordVerificationResult.Success)
                return false;

            if (!_context.Users.Any(u => u.UserName == credentials.UserName && u.Password == user.Password))
                return false;

            return true;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration.GetSection("TokenKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId",user.Id.ToString()),
                    new Claim("userName",user.UserName),
                    new Claim("name",user.Name),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "https://localhost",
                Audience = "CSS",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
