namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
