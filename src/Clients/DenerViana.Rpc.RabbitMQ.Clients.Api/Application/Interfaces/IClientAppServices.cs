using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;

public interface IClientAppServices
{
    Task<IEnumerable<ClientResponse>> GetAllAsync();
    Task<ClientDetailsResponse> GetByIdAsync(Guid id);

    Task<bool> AddAsync(ClientRequest request, UserInfoDto userInfo);
}
