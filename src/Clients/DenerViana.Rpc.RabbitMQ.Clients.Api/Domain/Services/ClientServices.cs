using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Services;

/// <summary>
/// Provides services for managing client-related operations.
/// </summary>
public class ClientServices(IClientRepository repository) : IClientServices
{
    #region Properties

    private readonly IClientRepository _repository = repository;

    #endregion

    #region Public Methods

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Client> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _repository.ExistsAsync(id);
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _repository.ExistsAsync(name);
    }

    public async Task<bool> AddAsync(Client client)
    {
        return await _repository.AddAsync(client);
    }

    #endregion
}
