using AutoMapper;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.AutoMapper;

public class MappingProfile : Profile
{
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
