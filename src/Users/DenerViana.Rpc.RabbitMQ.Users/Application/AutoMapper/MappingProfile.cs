using AutoMapper;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ReverseMap();
        CreateMap<User, RegisterUserRequest>().ReverseMap();
        CreateMap<User, UserDetailsResponse>().ReverseMap();
    }
}