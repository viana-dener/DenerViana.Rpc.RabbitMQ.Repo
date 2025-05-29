using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Events;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands;

public class UserCommandHandler(ILog log, INotify notify, IMediatorHandler mediatorHandler, IUserRepository repository) : CommandHandler, IRequestHandler<AddUserCommand, ValidationResult>
{
    #region Properties

    private readonly ILog _log = log;
    private readonly INotify _notify = notify;
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
    private readonly IUserRepository _repository = repository;

    #endregion

    #region Public Methods

    public async Task<ValidationResult> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        if (!await ValidateCommand(command)) return command.ValidationResult;

        var user = Create(command);

        // Save to database
        if (!await _repository.AddAsync(user))
        {
            _notify.AddError("Error adding user");
            return command.ValidationResult;
        }

        await PublishAvent(command, user);

        return command.ValidationResult;
    }

    #endregion

    #region Private Methods

    private async Task<bool> PublishAvent(AddUserCommand command, User user)
    {
        var result = true;

        // Log successful addition
        _log.Publish(LogLevel.Information, $"User {user.Name} added successfully by {command.CreatedBy}");

        // Publish event
        user.AddEvent(new UserAddedEvent(command.Origin, command.Name, command.TaxNumber, command.Email, command.Password, command.CorrelationId, command.CreatedId, command.CreatedBy));
        if (!await _mediatorHandler.PublishEvents(user.Events))
        {
            result = false;
            _log.Publish(LogLevel.Error, $"Error publishing user {user.Name} added event");
        }

        user.ClearEvents();
        _log.Publish(LogLevel.Information, $"User {user.Name} added event published successfully");

        return result;
    }

    private static User Create(AddUserCommand command)
    {
        // Create user
        return new User(command.Origin, command.Name, command.Email, command.Password, command.CreatedId, command.CreatedBy, Guid.Parse(command.CorrelationId));
    }

    private async Task<bool> ValidateCommand(AddUserCommand command)
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
        if (await _repository.ExistsAsync(command.Email.Trim()))
        {
            _notify.AddError("User already exists", 409);
            result = false;
        }

        return result;
    }

    #endregion
}

