using EduLearnAPI.Models;
using EduLearnAPI.Repositories;
using EduLearnAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EduLearnAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthController(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_userRepository.GetUserByEmail(user.Email) != null)
                return Ok("User already exists.");
            var userCount = _userRepository.GetUsersCount();
            user.Id = (int)(userCount + 1); 
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _userRepository.AddUser(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(string Email, string PasswordHash)
        {
            var user = _userRepository.GetUserByEmail(Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(PasswordHash, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new
            {
                UserId = user.Id,
                Token = token
            });
        }
    }
}
