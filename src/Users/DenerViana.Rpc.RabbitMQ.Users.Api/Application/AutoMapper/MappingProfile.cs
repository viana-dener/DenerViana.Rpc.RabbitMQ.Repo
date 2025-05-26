using AutoMapper;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.AutoMapper;

/// <summary>
/// AutoMapper profile that defines object-object mappings between domain entities and DTOs related to <see cref="User"/>.
/// Includes mappings for request and response models.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ReverseMap();
        CreateMap<User, UserRequest>().ReverseMap();
        CreateMap<User, UserDetailsResponse>().ReverseMap();
    }
}