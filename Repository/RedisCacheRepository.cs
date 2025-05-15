using System.Text.Json;
using RinhaDeBackEnd2023.Repository.Interfaces;
using StackExchange.Redis;

namespace RinhaDeBackEnd2023.Repository
{
    public class RedisCacheRepository : IRedisCacheRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var value = await db.StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);
        }
    }
}