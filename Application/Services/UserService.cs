using System;
using System.Threading.Tasks;
using Vidya.Domain.Entities;
//using Vidya.Infrastructure.Caching;
using Vidya.Application.Interfaces;
using Vidya.Core.Security;

namespace Vidya.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtService;
        //private readonly RedisCacheService _cacheService;

        public UserService(IUserRepository userRepository, JwtTokenService jwtService)// RedisCacheService cacheService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
          //  _cacheService = cacheService;
        }

        public async Task<string> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; // Authentication failed
            }

            // Generate JWT Token
            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.UserName);

            // Store token in Redis with expiration
           // await StoreTokenAsync(user.UserId.ToString(), token); // not using Radis

            return token;
        }

        //public async Task StoreTokenAsync(string userId, string token)
        //{
        //    await _cacheService.SetCacheAsync($"token_{userId}", token, TimeSpan.FromHours(1));
        //}

        public async Task StoreTokenAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("userId or token cannot be null or empty.");
            }

            // Make sure _cacheService is properly initialized and injected
           // await _cacheService.SetCacheAsync($"token_{userId}", token, TimeSpan.FromHours(1)); // 20-07-2025
        }

        //public async Task<string> GetStoredTokenAsync(string userId)
        //{
        //    return await _cacheService.GetCacheAsync($"token_{userId}");
        //}
    }
}
