using AutoMapper;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Services;

public class ClientAppServices(ILogInformation logInformation, INotify notify, IMapper mapper, IClientServices services) : IClientAppServices
{
    #region Properties

    private readonly ILogInformation _logInformation = logInformation;
    private readonly INotify _notify = notify;
    private readonly IMapper _mapper = mapper;
    private readonly IClientServices _services = services;

    #endregion

    #region Public Methods

    public async Task<IEnumerable<ClientResponse>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<ClientResponse>>(await _services.GetAllAsync());
    }
    public async Task<ClientDetailsResponse> GetByIdAsync(Guid id)
    {
        return _mapper.Map<ClientDetailsResponse>(await _services.GetByIdAsync(id));
    }
    public async Task<ClientDetailsResponse> GetByNameAsync(string name)
    {
        return _mapper.Map<ClientDetailsResponse>(await _services.GetByNameAsync(name));
    }
    public async Task<bool> AddAsync(ClientRequest request, UserInfoDto userInfo)
    {
        if (await _services.ExistsAsync(request.Name))
        {
            _notify.AddError("Client already exists", 409);
            return false;
        }

        var client = new Domain.Entities.Client(request.Id, userInfo.Origin, request.Name, request.Email, request.TaxNumber, userInfo.UserId, userInfo.UserName);
        if (client != null)
        {
            var address = ExtendedMethods.GenerateAddressDictionary();
            client.SetAddress(address["Street"].ToString(), address["Neighborhood"].ToString(), address["City"].ToString(), address["Region"].ToString(), address["Country"].ToString(), address["PostalCode"].ToString(), (double)address["Latitude"], (double)address["Longitude"]);
        }

        var result = await _services.AddAsync(client);
        if (!result)
        {
            _notify.AddError("Error adding client", 500);
            return false;
        }

         _logInformation.PublicherLog($"Client {client.Name} added with success by {userInfo.UserId}");

        // Lançar evento de integração: Client registrado event

        return result;
    }

    #endregion
}
