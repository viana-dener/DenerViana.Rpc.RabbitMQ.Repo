using AutoMapper;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Services;

/// <summary>
/// Application service responsible for user-related operations,
/// including querying users and adding new user accounts.
/// </summary>
public class UserAppServices(ILog log, INotify notify, IMapper mapper, IUserServices services) : IUserAppServices
{
    #region Properties

    private readonly ILog _log = log;
    private readonly INotify _notify = notify;
    private readonly IMapper _mapper = mapper;
    private readonly IUserServices _services = services;

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves all users asynchronously and maps them to UserResponse DTOs.
    /// </summary>
    /// <returns>A collection of UserResponse objects.</returns>
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<UserResponse>>(await _services.GetAllAsync());
    }

    /// <summary>
    /// Retrieves a user by unique identifier asynchronously and maps to UserDetailsResponse DTO.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <returns>User details as UserDetailsResponse.</returns>
    public async Task<UserDetailsResponse> GetByIdAsync(Guid id)
    {
        return _mapper.Map<UserDetailsResponse>(await _services.GetByIdAsync(id));
    }

    /// <summary>
    /// Retrieves a user by name asynchronously and maps to UserDetailsResponse DTO.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <returns>User details as UserDetailsResponse.</returns>
    public async Task<UserDetailsResponse> GetByNameAsync(string name)
    {
        return _mapper.Map<UserDetailsResponse>(await _services.GetByNameAsync(name));
    }

    /// <summary>
    /// Adds a new user asynchronously if the email does not already exist.
    /// Logs and notifies errors if operation fails.
    /// </summary>
    /// <param name="request">The user request data.</param>
    /// <param name="userInfo">Additional user information such as origin and creator details.</param>
    /// <returns>True if user is added successfully; otherwise, false.</returns>
    public async Task<bool> AddAsync(UserRequest request, UserInfoDto userInfo)
    {
        if (await _services.ExistsAsync(request.Email))
        {
            _notify.AddError("Account already exists");
            return false;
        }

        var account = new User(userInfo.Origin, request.Name, request.Email, request.Password, userInfo.UserId, userInfo.UserName);

        var result = await _services.AddAsync(account);
        if (!result)
        {
            _notify.AddError("Error adding user", 500);
            return false;
        }

        _log.Publish(LogLevel.Information, "Usuário registrado com sucesso!");

        // TODO: Publish integration event for User Registered

        return result;
    }

    #endregion
}