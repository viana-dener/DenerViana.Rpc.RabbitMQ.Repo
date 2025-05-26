using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

/// <summary>
/// Repository interface for managing <see cref="User"/> aggregate root entities.
/// Defines methods for querying, adding, and verifying user existence.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A collection of users.</returns>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Retrieves a user by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves a user by its name asynchronously.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User> GetByNameAsync(string name);

    /// <summary>
    /// Checks if a user exists by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier to check.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    /// Checks if a user exists by email asynchronously.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if the user exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(string email);

    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="account">The user entity to add.</param>
    /// <returns>True if the user was successfully added; otherwise, false.</returns>
    Task<bool> AddAsync(User account);

    /// <summary>
    /// Gets the unit of work instance for transaction management.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }
}
