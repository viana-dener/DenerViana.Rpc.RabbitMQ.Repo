namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
