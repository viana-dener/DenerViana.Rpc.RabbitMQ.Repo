using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cache;

public class CacheService<T>(IMemoryCache memoryCache) : ICacheService<T>
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public void Set(string key, T item, TimeSpan expiration)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        _memoryCache.Set(key, item, cacheEntryOptions);
    }

    public T Get(string key)
    {
        return _memoryCache.TryGetValue(key, out T value) ? value : default;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}
