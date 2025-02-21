using EduLearnAPI.Models;
using EduLearnAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EduLearnPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound("User not found.");

            user.Name = updatedUser.Name ?? user.Name;
            user.Email = updatedUser.Email ?? user.Email;
            user.MobileNumber = updatedUser.MobileNumber ?? user.MobileNumber;
            user.Bio = updatedUser.Bio ?? user.Bio;

            if (!string.IsNullOrWhiteSpace(updatedUser.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);
            }

            _userRepository.UpdateUser(user);
            return Ok("User updated successfully.");
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound("User not found.");

            _userRepository.DeleteUser(id);
            return Ok("User deleted successfully.");
        }

        [HttpGet]
        public IActionResult GetUsersCount()
        {
            var count = _userRepository.GetUsersCount();
            return Ok(count);
        }
    }
}
