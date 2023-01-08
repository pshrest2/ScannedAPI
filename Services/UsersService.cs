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
    }
}

