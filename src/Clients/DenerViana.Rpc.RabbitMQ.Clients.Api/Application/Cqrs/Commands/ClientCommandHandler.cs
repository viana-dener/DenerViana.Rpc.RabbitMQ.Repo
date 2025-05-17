using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Commands;

public class ClientCommandHandler : IRequestHandler<AddClientCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(AddClientCommand command, CancellationToken cancellationToken)
    {
        // Validate command
        if (!command.IsValid()) return command.ValidationResult;
                
        var client = new Client(command.Id, command.Origin, command.Name, command.Email, command.TaxNumber, command.CreatedId, command.CreatedBy);

        // Business validation

        // Save to database


        return command.ValidationResult;
    }
}