using System;
using System.Threading.Tasks;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Interfaces;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserDto> Get(LoginDto dto)
        {
            dto.HashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = await _usersRepository.Get(dto);
            if (user == null) throw new Exception("Invalid credentials");
            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone
            };
        }

        public async Task<Guid> Register(RegisterDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                PartitionKey = dto.LastName
            };
            await _usersRepository.Register(user);
            return user.Id;
        }
    }
}

