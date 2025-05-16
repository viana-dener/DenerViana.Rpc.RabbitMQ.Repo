using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Services;

public class UserServices(IUserRepository repository) : IUserServices
{

    #region Properties

    private readonly IUserRepository _repository = repository;

    #endregion

    #region Public Methods

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
    public async Task<User> GetByNameAsync(string name)
    {
        return await _repository.GetByNameAsync(name);
    }
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _repository.ExistsAsync(id);
    }
    public async Task<bool> ExistsAsync(string email)
    {
        return await _repository.ExistsAsync(email);
    }

    public async Task<bool> RegisterUserAsync(User account)
    {
       return await _repository.RegisterUserAsync(account);
    }

    #endregion
}

