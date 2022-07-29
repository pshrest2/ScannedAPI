using System.Threading.Tasks;
using ScannedAPI.Helpers;
using ScannedAPI.Repositories.Contexts.Interfaces;
using StackExchange.Redis;

namespace ScannedAPI.Repositories.Contexts
{
    public class RedisContext: IRedisContext
    {
        private IDatabase _database;

        public RedisContext(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<object> GetAsync(string key)
        {
            var serializedValue = await _database.StringGetAsync(key);
            return JsonSerializer.Deserialize(serializedValue);
        }
            

        public async Task SetAsync(string key, object value)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedValue);
        }
    }
}

