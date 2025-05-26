using AutoMapper;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Services;


/// <summary>
/// Provides application-level services to manage clients, including retrieval and addition operations.
/// </summary>
/// <param name="log">Logging service instance.</param>
/// <param name="notify">Notification service instance.</param>
/// <param name="mapper">Mapper instance for object mapping.</param>
/// <param name="services">Domain client services instance.</param>
public class ClientAppServices(ILog log, INotify notify, IMapper mapper, IClientServices services) : IClientAppServices
{
    #region Properties

    private readonly ILog _log = log;
    private readonly INotify _notify = notify;
    private readonly IMapper _mapper = mapper;
    private readonly IClientServices _services = services;

    #endregion

    #region Public Methods

    /// <summary>
    /// Retrieves all clients asynchronously.
    /// </summary>
    /// <returns>A collection of <see cref="ClientResponse"/> representing all clients.</returns>
    public async Task<IEnumerable<ClientResponse>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<ClientResponse>>(await _services.GetAllAsync());
    }

    /// <summary>
    /// Retrieves detailed client information by client ID asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the client.</param>
    /// <returns>A <see cref="ClientDetailsResponse"/> containing the client's detailed information.</returns>
    public async Task<ClientDetailsResponse> GetByIdAsync(Guid id)
    {
        return _mapper.Map<ClientDetailsResponse>(await _services.GetByIdAsync(id));
    }

    /// <summary>
    /// Retrieves detailed client information by client name asynchronously.
    /// </summary>
    /// <param name="name">The name of the client.</param>
    /// <returns>A <see cref="ClientDetailsResponse"/> containing the client's detailed information.</returns>
    public async Task<ClientDetailsResponse> GetByNameAsync(string name)
    {
        return _mapper.Map<ClientDetailsResponse>(await _services.GetByNameAsync(name));
    }

    /// <summary>
    /// Adds a new client asynchronously after validation and sets a default address.
    /// </summary>
    /// <param name="request">The client request data.</param>
    /// <param name="userInfo">Information about the user performing the operation.</param>
    /// <returns>True if the client was added successfully; otherwise, false.</returns>
    public async Task<bool> AddAsync(ClientRequest request, UserInfoDto userInfo)
    {
        // Validate if client already exists
        if (await _services.ExistsAsync(request.Name))
        {
            _notify.AddError("Client already exists", 409);
            return false;
        }

        // Create client entity
        var client = new Domain.Entities.Client(request.Id, userInfo.Origin, request.Name, request.Email, request.TaxNumber, userInfo.UserId, userInfo.UserName);
        if (client != null)
        {
            var address = ExtendedMethods.GenerateAddressDictionary();
            client.SetAddress(
                address["Street"].ToString(),
                address["Neighborhood"].ToString(),
                address["City"].ToString(),
                address["Region"].ToString(),
                address["Country"].ToString(),
                address["PostalCode"].ToString(),
                (double)address["Latitude"],
                (double)address["Longitude"]
            );
        }

        // Persist client entity
        var result = await _services.AddAsync(client);
        if (!result)
        {
            _notify.AddError("Error adding client", 400);
            return false;
        }

        // Log successful addition
        _log.Publish(LogLevel.Information, $"Client {client.Name} added successfully by {userInfo.UserId}");

        // Trigger integration event: Client registered event

        return result;
    }

    #endregion
}
