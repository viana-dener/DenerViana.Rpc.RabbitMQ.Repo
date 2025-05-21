using MediatR;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    public Event()
    {
        Timestamp = DateTime.UtcNow;
    }
}
