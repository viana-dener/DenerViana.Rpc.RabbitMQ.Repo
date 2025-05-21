using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;

/// <summary>
/// Defines application services related to client operations.
/// </summary>
public interface IClientAppServices
{
    /// <summary>
    /// Retrieves all clients asynchronously.
    /// </summary>
    /// <returns>A collection of client responses.</returns>
    Task<IEnumerable<ClientResponse>> GetAllAsync();

    /// <summary>
    /// Retrieves a client by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the client.</param>
    /// <returns>A detailed response of the requested client.</returns>
    Task<ClientDetailsResponse> GetByIdAsync(Guid id);

    /// <summary>
    /// Adds a new client asynchronously.
    /// </summary>
    /// <param name="request">The request containing client information.</param>
    /// <param name="userInfo">User information for tracking creation details.</param>
    /// <returns>A boolean value indicating success or failure.</returns>
    Task<bool> AddAsync(ClientRequest request, UserInfoDto userInfo);
}
