using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Contexts.Interfaces;

namespace ScannedAPI.Repositories.Context
{
    public class CosmosDbContext : ICosmosDbContext
    {
        private readonly Container _container;

        public CosmosDbContext(CosmosClient client, string databaseName, string containerName)
        {
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(ScannlyItem item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.PartitionKey));
        }

        public async Task DeleteItemAsync(string id)
        {
            await _container.DeleteItemAsync<ScannlyItem>(id, new PartitionKey(id));
        }

        public async Task<ScannlyItem> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<ScannlyItem> response = await _container.ReadItemAsync<ScannlyItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ScannlyItem>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<ScannlyItem>(new QueryDefinition(queryString));
            var results = new List<ScannlyItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, ScannlyItem item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}

