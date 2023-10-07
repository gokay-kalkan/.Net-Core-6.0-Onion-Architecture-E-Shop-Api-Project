
using Application.Dtos.UserDtos;
using Application.Features.Users.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Users.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();

            CreateMap<CreateUserDto, CreateUserCommand>().ReverseMap();

            CreateMap<User, CreateUserCommand>().ReverseMap();

            CreateMap<User, LoginUserDto>().ReverseMap();

            CreateMap<User, LoginUserCommand>().ReverseMap();

            CreateMap<LoginUserDto, LoginUserCommand>().ReverseMap();
        
        }
    }
}
