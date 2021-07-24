using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.Pricing.HeatPricingService.Services
{

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public double GetCachedPrice()
        {
            double cachePrice;

            if (!_cache.TryGetValue(CacheKeys.Price, out cachePrice))
            {
                // Key not in cache, so get data.
                cachePrice = 1;

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20));

                // Save data in cache.
                _cache.Set(CacheKeys.Price, cachePrice, cacheEntryOptions);
            }

            return cachePrice;
        }

        public void UpdateCachedPrice(double value)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromSeconds(20));

            _cache.Set(CacheKeys.Price, value, cacheEntryOptions);
        }

        public void ClearCache()
        {
            _cache.Remove(CacheKeys.Price);
        }
    }
}
