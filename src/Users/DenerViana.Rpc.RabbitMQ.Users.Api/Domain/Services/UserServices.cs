using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Services;

/// <summary>
/// Implements user-related business operations by interacting with the user repository.
/// Provides methods to retrieve users, check existence, and add new users.
/// </summary>
public class UserServices(IUserRepository repository) : IUserServices
{
    #region Properties

    private readonly IUserRepository _repository = repository;

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves all users asynchronously.
    /// </summary>
    /// <returns>A collection of users.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    /// <summary>
    /// Retrieves a user by unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Retrieves a user by name asynchronously.
    /// </summary>
    /// <param name="name">The user's name.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    /// <summary>
    /// Checks if a user exists by unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier to check.</param>
    /// <returns>True if user exists; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _repository.ExistsAsync(id);
    }

    /// <summary>
    /// Checks if a user exists by email asynchronously.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if user exists; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(string email)
    {
        return await _repository.ExistsAsync(email);
    }

    /// <summary>
    /// Adds a new user asynchronously.
    /// </summary>
    /// <param name="account">The user entity to add.</param>
    /// <returns>True if the user was successfully added; otherwise, false.</returns>
    public async Task<bool> AddAsync(User account)
    {
        return await _repository.AddAsync(account);
    }

    #endregion
}
