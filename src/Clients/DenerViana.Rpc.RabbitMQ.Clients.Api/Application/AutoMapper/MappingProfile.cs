using AutoMapper;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.AutoMapper;

/// <summary>
/// Defines mapping profiles for object transformations using AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class
    /// and sets up mappings between domain models and DTOs.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Client, ClientResponse>().ReverseMap();
        CreateMap<Client, ClientDetailsResponse>().ReverseMap();
        CreateMap<Client, ClientRequest>().ReverseMap();

        CreateMap<Address, AddressRequest>().ReverseMap();
        CreateMap<Address, AddressResponse>().ReverseMap();
        CreateMap<Address, ClientDetailsResponse>().ReverseMap();
    }
}
