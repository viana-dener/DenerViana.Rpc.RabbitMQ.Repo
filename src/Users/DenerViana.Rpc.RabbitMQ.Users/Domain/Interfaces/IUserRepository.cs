using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;

namespace DenerViana.Rpc.RabbitMQ.Users.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByNameAsync(string name);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(string email);

    Task<bool> RegisterUserAsync(User account);

    IUnitOfWork UnitOfWork { get; }
}
