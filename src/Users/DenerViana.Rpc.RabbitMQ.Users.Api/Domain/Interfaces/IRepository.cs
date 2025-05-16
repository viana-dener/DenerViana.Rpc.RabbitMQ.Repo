using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{

}
