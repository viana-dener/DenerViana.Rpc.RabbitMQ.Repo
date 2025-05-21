using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

/// <summary>
/// Interface for client service operations.
/// Defines methods for retrieving, checking existence, and adding client records.
/// </summary>
public interface IClientServices
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
    /// Checks if a client exists by its name.
    /// </summary>
    Task<bool> ExistsAsync(string name);

    /// <summary>
    /// Adds a new client to the service.
    /// </summary>
    Task<bool> AddAsync(Client client);
}
