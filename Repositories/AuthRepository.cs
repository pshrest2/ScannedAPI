using System;
using System.Threading.Tasks;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Contexts.Interfaces;
using ScannedAPI.Repositories.Interfaces;
using User = ScannedAPI.Models.User;

namespace ScannedAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ICosmosDbContext _cosmosDbContext;

        public AuthRepository(ICosmosDbContext cosmosDbContext)
        {
            _cosmosDbContext = cosmosDbContext;
        }

        public async Task Register(User user)
        {
            await _cosmosDbContext.AddItemAsync(new ScannlyItem()
            {
                Id = Guid.NewGuid().ToString(),
                User = user
            });
            Console.WriteLine($"{string.Concat(user.FirstName, user.MiddleName, user.LastName)} has been registered");
        }
    }
}

