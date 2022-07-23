using System;
using System.Linq;
using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Contexts.Interfaces;
using ScannedAPI.Repositories.Interfaces;

namespace ScannedAPI.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ICosmosDbContext _cosmosDbContext;

        public UsersRepository(ICosmosDbContext cosmosDbContext)
        {
            _cosmosDbContext = cosmosDbContext;
        }

        public async Task<User> Get(LoginDto dto)
        {
            var query = $@"SELECT * FROM scannlyItems si WHERE si.user.Email = {dto.Email} AND si.user.Password = {dto.HashedPassword}";
            var scannlyItems = await _cosmosDbContext.GetItemsAsync(query);
            return scannlyItems.FirstOrDefault()?.User;
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

