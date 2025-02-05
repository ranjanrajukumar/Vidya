using Vidya.API.DTO;  // Assuming DTOs are defined here
using Vidya.Application.Interfaces;
using Vidya.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidya.Core.Security;
using Vidya.Application.Services;



namespace Vidya.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtTokenService _jwtService;
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

       

        // Inject IUserRepository in the constructor
        public UserController(JwtTokenService jwtService, UserService userService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userService = userService;
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null)// || users.Count == 0)
                return NotFound("No users found.");

            var userDtos = users.Select(user => new UserDTO
            {
                //UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                RoleID = user.RoleID,
                IsAdmin = user.IsAdmin,
                Category = user.Category
            }).ToList();

            return Ok(userDtos);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            var userDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleID = user.RoleID,
                IsAdmin = user.IsAdmin,
                Category = user.Category
            };

            return Ok(userDto);
        }

    

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required.");

            if (string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Username and Password are required.");

            var user = new Users
            {
                UserName = userDto.UserName,
                Password = HashPassword(userDto.Password), // Hash the password
                RoleID = userDto.RoleID,
                IsAdmin = userDto.IsAdmin,
                Category = userDto.Category,
                AuthAdd = userDto.AuthAdd,
                AuthLstEdt = userDto.AuthLstEdt,
                AuthDel = userDto.AuthDel,
                AddOnDt = DateTime.UtcNow,
                DelStatus = 0
            };

            await _userRepository.AddUserAsync(user);

            var createdUserDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleID = user.RoleID,
                IsAdmin = user.IsAdmin,
                Category = user.Category
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, createdUserDto);
        }

        // Example password hashing function
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); // Requires BCrypt.Net-Next package
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required.");

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            // Update user fields
            user.UserName = userDto.UserName;
            user.RoleID = userDto.RoleID;
            user.IsAdmin = userDto.IsAdmin;
            user.Category = userDto.Category;
            user.EditOnDt = DateTime.UtcNow;

            // Hash password if updating
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            await _userRepository.UpdateUserAsync(user);

            // Return updated user details
            var updatedUserDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                RoleID = user.RoleID,
                IsAdmin = user.IsAdmin,
                Category = user.Category
            };

            return Ok(updatedUserDto);
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            await _userRepository.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginUser)
        {
            if (loginUser == null || string.IsNullOrWhiteSpace(loginUser.UserName) || string.IsNullOrWhiteSpace(loginUser.Password))
                return BadRequest(new { message = "Username and password are required." });

            // Fetch user from database
            var user = await _userRepository.GetUserByUsernameAsync(loginUser.UserName);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials." });

            // Validate password (hashed comparison)
            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
                return Unauthorized(new { message = "Invalid credentials." });

            //// Check if an active token already exists
            //var existingToken = await _userService.GetTokenAsync(user.UserId.ToString(), user.UserName);
            //if (!string.IsNullOrEmpty(existingToken))
            //    return Ok(new { token = existingToken });

            // Generate JWT token
            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.UserName);

            // Store token in Redis cache
            await _userService.StoreTokenAsync(user.UserId.ToString(), token);

            return Ok(new { token });
        }


        [HttpGet("token/{userId}")]
        public async Task<IActionResult> GetStoredToken(string userId)
        {
            var token = await _userService.GetStoredTokenAsync(userId);
            if (string.IsNullOrEmpty(token))
                return NotFound("Token not found.");

            return Ok(new { token });
        }

    }
}
