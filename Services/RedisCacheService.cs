using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreRedisCachingDemo.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> ExistsAsync(string cacheKey)
        {
            var element = await _cache.GetAsync(cacheKey);

            return element != null;
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var elementBytes = await _cache.GetAsync(cacheKey);
            var elementString = Encoding.UTF8.GetString(elementBytes);
            return JsonConvert.DeserializeObject<T>(elementString);
        }

        public async Task SetAsync<T>(string cacheKey, T element, DistributedCacheEntryOptions options)
        {
            var elementString = JsonConvert.SerializeObject(element);
            var elementBytes = Encoding.UTF8.GetBytes(elementString);
            await _cache.SetAsync(cacheKey, elementBytes, options);
        }

        public async Task SetAsync<T>(string cacheKey, T element)
        {
            await SetAsync(cacheKey, element, new DistributedCacheEntryOptions());
        }

        public async Task SetAsync<T>(string cacheKey, T element, DateTimeOffset absoluteExpiration)
        {
            await SetAsync(cacheKey, element, new DistributedCacheEntryOptions { AbsoluteExpiration = absoluteExpiration });
        }

        public async Task SetAsync<T>(string cacheKey, T element, TimeSpan expiryInterval)
        {
            await SetAsync(cacheKey, element, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiryInterval });
        }
    }
}
