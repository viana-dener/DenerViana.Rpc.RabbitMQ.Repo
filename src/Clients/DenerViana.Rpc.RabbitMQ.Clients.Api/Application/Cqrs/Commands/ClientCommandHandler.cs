using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Events;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Commands;

/// <summary>
/// Handles client-related commands and processes business logic.
/// </summary>
public class ClientCommandHandler(ILog log, INotify notify, IMediatorHandler mediatorHandler, IClientRepository repository) : CommandHandler, IRequestHandler<AddClientCommand, ValidationResult>
{
    #region Properties

    private readonly ILog _log = log;
    private readonly INotify _notify = notify;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
    private readonly IClientRepository _repository = repository;

    #endregion

    #region Public Methods

    public async Task<ValidationResult> Handle(AddClientCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommand(command)) return command.ValidationResult;

        var client = CreateClient(command);

        // Save to database
        if (!await _repository.AddAsync(client))
        {
            _notify.AddError("Error adding client");
            return command.ValidationResult;
        }

        await PublishAvent(command, client);

        return command.ValidationResult;
    }

    #endregion

    #region Private Methods

    private async Task<bool> PublishAvent(AddClientCommand command, Client client)
    {
        var result = true;

        // Log successful addition
        _log.Publish(LogLevel.Information, $"Client {client.Name} added successfully by {command.CreatedBy}");

        // Publish event
        client.AddEvent(new ClientAddedEvent(command.Id, command.Origin, command.Name, command.Email, command.TaxNumber, command.CorrelationId, command.CreatedId, command.CreatedBy));
        if (!await _mediatorHandler.PublishEvents(client.Events))
        {
            result = false;
            _log.Publish(LogLevel.Error, $"Error publishing client {client.Name} added event");
        }

        client.ClearEvents();
        _log.Publish(LogLevel.Information, $"Client {client.Name} added event published successfully");

        return result;
    }

    private static Client CreateClient(AddClientCommand command)
    {
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

        return client;
    }

    private async Task<bool> ValidateCommand(AddClientCommand command)
    {
        var result = true;

        // Validate command
        if (!command.IsValid())
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                _notify.AddError(error.ErrorMessage);
            }

            return false;
        }

        // Business validation
        if (await _repository.ExistsAsync(command.TaxNumber))
        {
            _notify.AddError("Client already exists", 409);
            result = false;
        }

        return result;
    }

    #endregion
}
