using System;
using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Interfaces;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Guid> Register(RegisterDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PartitionKey = dto.LastName
            };
            var userId = await _authRepository.Register(user);
            return userId;
        }
    }
}

