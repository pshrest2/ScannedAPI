using System;
using System.Threading.Tasks;
using AutoMapper;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;
using ScannedAPI.Models;
using ScannedAPI.Repositories.Interfaces;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        public UsersService(IMapper mapper, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<UserDto> Get(LoginDto dto)
        {
            var user = await _usersRepository.Get(dto);
            if (user == null) throw new Exception("User does not exist");

            var passwordMatches = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordMatches) throw new Exception("Invalid Credentials");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<Guid> Register(RegisterDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Id = Guid.NewGuid();
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _usersRepository.Register(user);
            return user.Id;
        }
    }
}

