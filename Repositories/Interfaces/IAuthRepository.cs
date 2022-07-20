using System;
using System.Threading.Tasks;
using ScannedAPI.Models;

namespace ScannedAPI.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<Guid> Register(User user);
    }
}

