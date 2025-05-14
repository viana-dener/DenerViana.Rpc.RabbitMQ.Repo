namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

public interface ICacheService<T>
{
    void Set(string key, T item, TimeSpan expiration);
    T Get(string key);
    void Remove(string key);
}
