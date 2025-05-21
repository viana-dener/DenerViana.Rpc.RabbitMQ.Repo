namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using Microsoft.Extensions.Logging;

public interface ILog
{
    void Publish(LogLevel level, string message);
}
