using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.Users;
using QuestPlannerAPI.Data;
using QuestPlannerAPI.Models;
using QuestPlannerAPI.Services;

namespace QuestPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public UsersController(AppDbContext context, IAuthService authService, IConfiguration config)
        {
            _db = context;
            _authService = authService;
            _config = config;
        }

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(Users newUser)
        {
            if (_db.Users.FirstOrDefault(u => u.Email == newUser.Email) == null)
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
                string token = _authService.Login(newUser);
                return StatusCode(201, new { token });
            }   
            else
            {
                return BadRequest("User already exists");   
            }
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] UsersDTO request)
        {
            var token = _authService.Login(new Users { Email = request.Email, Password = request.Password });
            if (token == null) return Unauthorized("invalid username or password");

            return Ok(new { token });
        }

        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken([FromHeader] string authorization)
        {
            if (string.IsNullOrEmpty(authorization))
            {
                return Unauthorized(new { valid = false, message = "No token provided." });
            }

            var token = authorization.Replace("Bearer ", "").Trim();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidAudience = _config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:key"]))
                }, out SecurityToken validatedToken);

                return Ok(new { valid = true, message = "Token is valid." });
            }
            catch (Exception)
            {          
                return Unauthorized(new { valid = false, message = "Invalid or expired token." });
            }
        }

        [Authorize]
        [HttpGet("getProfile")]
        public IActionResult GetProfile()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                Users? user = _db.Users.Find(int.Parse(userId));
                return Ok(new { username = user.Username, email = user.Email });
            }
            else
            {
                 return NotFound("User not found");
            }
        }
    }
}

