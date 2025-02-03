using System.Threading.Tasks;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Caching;
using Vidya.Domain.Entities;
using Vidya.Infrastructure.Caching;

namespace Vidya.Application.Services
{
    public class UserService
    {
        private readonly RedisCacheService _cacheService;

        public UserService(RedisCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<bool> ValidateUserAsync(Users user, string password)
        {
            // Simulate password check (hashing should be implemented in production)
            return user.Password == password;
        }

        public async Task StoreTokenAsync(string userId, string token)
        {
            await _cacheService.SetCacheAsync($"token_{userId}", token, TimeSpan.FromHours(1));
        }

        public async Task<string> GetStoredTokenAsync(string userId)
        {
            return await _cacheService.GetCacheAsync($"token_{userId}");
        }
    }
}
