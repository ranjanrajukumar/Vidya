using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Vidya.Core.Security;
using Vidya.Application.Services;
using Vidya.Domain.Entities;


namespace Vidya.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtService;
        private readonly UserService _userService;

        public AuthController(JwtTokenService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users loginUser)
        {
            //Simulate a user(Replace with database lookup)
            var user = new Users { UserId = 1, UserName = "test@example.com", Password = "password123" };

            if (!await _userService.ValidateUserAsync(user, loginUser.Password))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.UserName);
            await _userService.StoreTokenAsync(user.UserId.ToString(), token);

            return Ok(new { token });
        }

        [HttpGet("token/{userId}")]
        public async Task<IActionResult> GetStoredToken(string userId)
        {
            var token = await _userService.GetStoredTokenAsync(userId);
            if (string.IsNullOrEmpty(token))
                return NotFound("Token not found");

            return Ok(new { token });
        }
    }
}
