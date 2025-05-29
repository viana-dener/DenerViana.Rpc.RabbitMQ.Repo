using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using FluentValidation.Results;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Integration.Response;

public class ResponseMessage(ValidationResult validationResult) : Message
{
    public ValidationResult ValidationResult { get; set; } = validationResult;
}
