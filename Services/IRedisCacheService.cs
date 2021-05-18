using System;
using System.Threading.Tasks;

namespace AspNetCoreRedisCachingDemo.Services
{
    public interface IRedisCacheService
    {
        Task<bool> ExistsAsync(string cacheKey);
        Task<T> GetAsync<T>(string cacheKey);
        Task SetAsync<T>(string cacheKey, T element);
        Task SetAsync<T>(string cacheKey, T element, DateTimeOffset absoluteExpiration);
        Task SetAsync<T>(string cacheKey, T element, TimeSpan expiryInterval);
    }
}
