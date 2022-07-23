using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Models;

namespace ScannedAPI.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> Get(LoginDto dto);
        Task Register(User user);
    }
}

