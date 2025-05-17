using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;

    public async Task PublishEvent<T>(T @event) where T : Event
    {
        await _mediator.Publish(@event);
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}
