using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace URLShortener.Core.Utilities
{
    public static class CacheUtility
    {
        public static async Task<T?> GetOrAddAsync<T>(this IDistributedCache cache, string cacheKey, Func<Task<T>> fetchData)
        {
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            var data = await fetchData();
            if (data != null)
            {
                await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(data));
            }

            return data;
        }
    }
}
