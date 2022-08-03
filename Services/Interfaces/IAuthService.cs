using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;

namespace ScannedAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> ValidateUser(LoginDto dto);
    }
}

