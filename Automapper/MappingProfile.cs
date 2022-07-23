using AutoMapper;
using ScannedAPI.Dtos.AuthDtos;
using ScannedAPI.Dtos.User;
using ScannedAPI.Models;

namespace ScannedAPI.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<RegisterDto, User>();
        }
    }
}

