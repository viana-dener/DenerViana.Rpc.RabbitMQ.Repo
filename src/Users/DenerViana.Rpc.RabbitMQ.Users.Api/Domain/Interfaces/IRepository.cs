using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

/// <summary>
/// Generic repository interface for aggregate root entities.
/// Provides a contract for data access operations and resource cleanup.
/// </summary>
/// <typeparam name="T">The type of the aggregate root entity.</typeparam>
public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
}
