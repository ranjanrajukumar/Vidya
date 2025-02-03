using System;
using StackExchange.Redis;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Vidya.Infrastructure.Caching
{
    public class RedisCacheService
    {
        private readonly IDatabase _cacheDb;

        public RedisCacheService(IConfiguration config)
        {
            var redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
            _cacheDb = redis.GetDatabase();
        }

        public async Task SetCacheAsync(string key, string value, TimeSpan expiry)
        {
            await _cacheDb.StringSetAsync(key, value, expiry);
        }

        public async Task<string> GetCacheAsync(string key)
        {
            return await _cacheDb.StringGetAsync(key);
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cacheDb.KeyDeleteAsync(key);
        }
    }
}
