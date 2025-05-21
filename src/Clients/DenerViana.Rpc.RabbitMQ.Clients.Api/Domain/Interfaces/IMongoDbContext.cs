using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using MongoDB.Driver;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

/// <summary>
/// Interface for MongoDB context management.
/// Provides access to the Client collection and a commit operation.
/// </summary>
public interface IMongoDbContext: IDisposable
{
    /// <summary>
    /// Commits changes asynchronously.
    /// </summary>
    Task<bool> CommitAsync();

    /// <summary>
    /// Gets the MongoDB collection for clients.
    /// </summary>
    IMongoCollection<Client> Clients { get; }
}
