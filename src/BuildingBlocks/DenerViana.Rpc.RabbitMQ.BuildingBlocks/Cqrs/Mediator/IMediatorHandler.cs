using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using FluentValidation.Results;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;

public interface IMediatorHandler
{
    Task<bool> PublishEvent<T>(T @event) where T : Event;
    Task<bool> PublishEvents<T>(IReadOnlyCollection<T> events) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command; 
}
