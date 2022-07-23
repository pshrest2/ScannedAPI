using System;
using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;

namespace ScannedAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto> Get(LoginDto dto);
        Task<Guid> Register(RegisterDto user);
    }
}

