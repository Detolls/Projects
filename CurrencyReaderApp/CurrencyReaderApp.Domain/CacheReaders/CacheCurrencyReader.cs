using CurrencyReaderApp.Domain.Interfaces;
using System;
using System.Linq;
using System.Runtime.Caching;

namespace CurrencyReaderApp.Domain.CacheReaders
{
    /// <summary>
    /// Decorator for <seealso cref="ICurrencyReader"/> which caches items.
    /// </summary>
    public class CacheCurrencyReader : ICurrencyReader
    {
        private readonly ICurrencyReader component;
        private const int CacheLimitSize = 10;

        public CacheCurrencyReader(ICurrencyReader component)
        {
            this.component = component ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// The method extends the component's method using cache.
        /// </summary>
        /// <returns>Currency.</returns>
        public decimal GetCurrencyByDate(DateTime date)
        {
            MemoryCache cache = MemoryCache.Default;
            CacheItem cacheItem = cache.GetCacheItem(date.ToShortDateString());

            decimal currency;

            if (cacheItem == null)
            {
                currency = component.GetCurrencyByDate(date);

                if (cache.GetCount() == CacheLimitSize)
                {
                    cache.Remove(cache.First().Key);
                }

                cache.Add(date.ToShortDateString(), currency, new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, 5, 0) });

                return currency;
            }
            else
            {
                currency = Convert.ToDecimal(cacheItem.Value);

                return currency;
            }
        }
    }
}
