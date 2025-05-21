using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

/// <summary>
/// Interface for client repository operations.
/// Defines methods for retrieving, checking existence, and adding client records.
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    /// <summary>
    /// Retrieves all clients.
    /// </summary>
    Task<IEnumerable<Client>> GetAllAsync();

    /// <summary>
    /// Retrieves a client by its unique identifier.
    /// </summary>
    Task<Client> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves a client by its name.
    /// </summary>
    Task<Client> GetByNameAsync(string name);

    /// <summary>
    /// Checks if a client exists by its unique identifier.
    /// </summary>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    /// Checks if a client exists by its tax number.
    /// </summary>
    Task<bool> ExistsAsync(string taxNumber);

    /// <summary>
    /// Adds a new client to the repository.
    /// </summary>
    Task<bool> AddAsync(Client product);
}
