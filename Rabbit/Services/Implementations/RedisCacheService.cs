using Microsoft.Extensions.Caching.Distributed;
using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.Services.Interfaces;
using System.Text.Json;

namespace Rabbit.Services.Implementations
{
    public class RedisCacheService : IRedisCacheService, ITransientService
    {
        private readonly IDistributedCache? _cache;

        public RedisCacheService(IDistributedCache? cache)
        {
            _cache = cache;
        }
        public T? GetData<T>(string key)
        {
            var data = _cache?.GetString(key);

            if(data == null)
                return default(T);

            return JsonSerializer.Deserialize<T>(data);
        }

        public void SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            _cache.SetString(key, JsonSerializer.Serialize(data));
        }
    }
}
