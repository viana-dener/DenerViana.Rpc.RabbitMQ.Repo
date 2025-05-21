using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Events;

/// <summary>
/// Handles client-related domain events, specifically when a new client is added.
/// Implements logic to respond to the ClientAddedEvent notification.
/// </summary>
public class ClientEventHandler : INotificationHandler<ClientAddedEvent>
{
    public Task Handle(ClientAddedEvent notification, CancellationToken cancellationToken)
    {
        // Send confirmation event
        return Task.CompletedTask;
    }
}
