using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Events;

public class UserEventHandler : INotificationHandler<UserAddedEvent>
{
    public Task Handle(UserAddedEvent notification, CancellationToken cancellationToken)
    {
        // Send confirmation event
        return Task.CompletedTask;
    }
}
