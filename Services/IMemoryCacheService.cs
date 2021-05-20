using System;
using System.Threading.Tasks;

namespace AspNetCoreRedisCachingDemo.Services
{
    public interface IMemoryCacheService
    {
        bool Exists<T>(string cacheKey);
        T Get<T>(string cacheKey);
        void Set<T>(string cacheKey, T element);
        void Set<T>(string cacheKey, T element, DateTimeOffset absoluteExpiration);
        void Set<T>(string cacheKey, T element, TimeSpan expiryInterval);
    }
}
