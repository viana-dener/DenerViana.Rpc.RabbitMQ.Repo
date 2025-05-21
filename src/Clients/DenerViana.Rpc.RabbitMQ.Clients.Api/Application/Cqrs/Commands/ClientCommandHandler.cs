using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Commands;

/// <summary>
/// Handles client-related commands and processes business logic.
/// </summary>
public class ClientCommandHandler(ILog log, IClientRepository repository) : CommandHandler, IRequestHandler<AddClientCommand, ValidationResult>
{
    private readonly ILog _log = log;
    private readonly IClientRepository _repository = repository;

    public async Task<ValidationResult> Handle(AddClientCommand command, CancellationToken cancellationToken)
    {
        // Validate command
        if (!command.IsValid()) return command.ValidationResult;

        // Business validation
        if (await _repository.ExistsAsync(command.TaxNumber))
        {
            AddError("Client already exists");
            return command.ValidationResult;
        }

        // Create client
        var client = new Client(command.Id, command.Origin, command.Name, command.Email, command.TaxNumber, command.CreatedId, command.CreatedBy);
        if (client != null)
        {
            var address = ExtendedMethods.GenerateAddressDictionary();

            client.SetAddress(
                address["Street"].ToString(),
                address["Neighborhood"].ToString(),
                address["City"].ToString(),
                address["Region"].ToString(),
                address["Country"].ToString(),
                address["PostalCode"].ToString(),
                (double)address["Latitude"],
                (double)address["Longitude"]
            );
        }

        // Save to database
        if (!await _repository.AddAsync(client))
        {
            AddError("Error adding client");
            return command.ValidationResult;
        }

        // Log successful addition
        _log.Publish(LogLevel.Information, $"Client {client.Name} added successfully by {command.CreatedBy}");

        // Publish event

        return command.ValidationResult;
    }
}
