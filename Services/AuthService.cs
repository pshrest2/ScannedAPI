using System;
using System.Threading.Tasks;
using AutoMapper;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;
using ScannedAPI.Repositories.Interfaces;
using ScannedAPI.Services.Interfaces;

namespace ScannedAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public AuthService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            
        }

        public async Task<UserDto> ValidateUser(LoginDto dto)
        {
            var user = await _usersRepository.Get(dto);
            if (user == null) throw new Exception("User does not exist");

            var passwordMatches = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordMatches) throw new Exception("Invalid Credentials");

            return _mapper.Map<UserDto>(user);
        }
    }
}

