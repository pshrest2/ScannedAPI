using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using ScannedAPI.Repositories.Interfaces;
using User = ScannedAPI.Models.User;

namespace ScannedAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Container _container;

        public AuthRepository(Container container)
        {
            _container = container;
        }

        public async Task<Guid> Register(User user)
        {
            ItemResponse<User> registeredUser = await _container.CreateItemAsync(user, new PartitionKey(user.PartitionKey));

            Console.WriteLine($@"Registerd user in database with id: {registeredUser.Resource.Id}
            Operation consumed {registeredUser.RequestCharge} RUs.");

            return registeredUser.Resource.Id;
        }
    }
}

