using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

/// <summary>
/// Service interface for managing user business operations.
/// Provides methods for retrieving, checking existence, and adding users.
/// </summary>
public interface IUserServices
{
    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A collection of users.</returns>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Retrieves a user by unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves a user by name asynchronously.
    /// </summary>
    /// <param name="name">The user's name.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    Task<User> GetByNameAsync(string name);

    /// <summary>
    /// Checks if a user exists by unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier to check.</param>
    /// <returns>True if user exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(Guid id);

    /// <summary>
    /// Checks if a user exists by email asynchronously.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if user exists; otherwise, false.</returns>
    Task<bool> ExistsAsync(string email);

    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="account">The user entity to add.</param>
    /// <returns>True if the user was successfully added; otherwise, false.</returns>
    Task<bool> AddAsync(User account);
}
