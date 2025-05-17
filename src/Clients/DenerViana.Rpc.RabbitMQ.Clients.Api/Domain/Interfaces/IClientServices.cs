using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;

public interface IClientServices
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client> GetByIdAsync(Guid id);
    Task<Client> GetByNameAsync(string name);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(string name);
    Task<bool> AddAsync(Client client);
}
