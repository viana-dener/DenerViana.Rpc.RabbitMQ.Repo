using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;

/// <summary>
/// Defines application-level services for managing users,
/// including retrieval and creation operations.
/// </summary>
public interface IUserAppServices
{
    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A collection of <see cref="UserResponse"/> DTOs.</returns>
    Task<IEnumerable<UserResponse>> GetAllAsync();

    /// <summary>
    /// Retrieves detailed information for a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>A <see cref="UserDetailsResponse"/> DTO if found; otherwise, null.</returns>
    Task<UserDetailsResponse> GetByIdAsync(Guid id);

    /// <summary>
    /// Retrieves detailed information for a user by their name.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <returns>A <see cref="UserDetailsResponse"/> DTO if found; otherwise, null.</returns>
    Task<UserDetailsResponse> GetByNameAsync(string name);

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="request">The user data to add.</param>
    /// <param name="userInfo">The information about the user performing the operation.</param>
    /// <returns>True if the user was added successfully; otherwise, false.</returns>
    Task<bool> AddAsync(UserRequest request, UserInfoDto userInfo);
}

