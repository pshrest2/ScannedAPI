using System.Collections.Generic;
using System.Threading.Tasks;
using ScannedAPI.Models;

namespace ScannedAPI.Repositories.Contexts.Interfaces
{
    public interface ICosmosDbContext
    {
        Task<IEnumerable<ScannlyItem>> GetItemsAsync(string query);
        Task<ScannlyItem> GetItemAsync(string id);
        Task AddItemAsync(ScannlyItem item);
        Task UpdateItemAsync(string id, ScannlyItem item);
        Task DeleteItemAsync(string id);
    }
}

