using System.Threading.Tasks;

namespace ScannedAPI.Repositories.Contexts.Interfaces
{
    public interface IRedisContext
    {
        Task<object> GetAsync(string key);
        Task SetAsync(string key, object value);
    }
}

