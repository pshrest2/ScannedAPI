using System;
using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;

namespace ScannedAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<Guid> Register(RegisterDto user);
    }
}

