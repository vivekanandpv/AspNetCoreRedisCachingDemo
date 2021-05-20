using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreRedisCachingDemo.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool Exists<T>(string cacheKey)
        {
            var element = _cache.Get<T>(cacheKey);

            return element != null;
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }

        private void Set<T>(string cacheKey, T element, MemoryCacheEntryOptions options)
        {
            _cache.Set(cacheKey, element, options);
        }

        public void Set<T>(string cacheKey, T element)
        {
            Set(cacheKey, element, new MemoryCacheEntryOptions());
        }

        public void Set<T>(string cacheKey, T element, DateTimeOffset absoluteExpiration)
        {
            Set(cacheKey, element, new MemoryCacheEntryOptions { AbsoluteExpiration = absoluteExpiration });
        }

        public void Set<T>(string cacheKey, T element, TimeSpan expiryInterval)
        {
            Set(cacheKey, element, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiryInterval });
        }
    }
}
