using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Domain.Interfaces;

public interface IUserServices
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByNameAsync(string name);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(string email);

    Task<bool> RegisterUserAsync(User account);
}
