

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.PersistentDataStorages
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _distributedCache;

        public SessionService(IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _distributedCache = distributedCache;
        }

        public async Task<string> GetAsync(string key)
        {
            var sessionValue = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(sessionValue))
            {
                sessionValue = _httpContextAccessor.HttpContext.Session.GetString(key);
                if (!string.IsNullOrEmpty(sessionValue))
                {
                    // Oturum verisi bulunduğunda, bu veriyi dağıtılmış önbelleğe kaydet
                    await _distributedCache.SetStringAsync(key, sessionValue, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30) // Örnek olarak 30 gün
                    });
                }
            }

            return sessionValue;
        }

        public async Task SetAsync(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
            await _distributedCache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30) // Örnek olarak 30 gün
            });
        }
    }
}
