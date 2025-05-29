using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Endipoints;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Helpers;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Notifications;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.AutoMapper;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Events;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Services;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Services;
using DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Repository;
using DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Validations;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.IoC;

/// <summary>
/// Provides extension methods to configure Dependency Injection (IoC) for the application.
/// Registers infrastructure, application, domain services, and repositories into the IServiceCollection.
/// </summary>
public static class BootStrapper
{
    /// <summary>
    /// Adds the IoC container setup for services used in the application.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The updated IServiceCollection with registered services.</returns>
    public static IServiceCollection AddIocSetup(this IServiceCollection services)
    {
        // Infrastructure
        services.AddScoped<IMainEndpoints, MainEndpoints>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddTransient<IValidator<UserRequest>, UserRouteValidator>();

        // Applications
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IUserAppServices, UserAppServices>();
        services.AddScoped<IRequestHandler<AddUserCommand, ValidationResult>, UserCommandHandler>();
        services.AddScoped<INotificationHandler<UserAddedEvent>, UserEventHandler>();

        // Domain
        services.AddScoped<INotify, Notify>();
        services.AddScoped<ILog, Log>();
        services.AddScoped<IUserServices, UserServices>();

        // Repository
        services.AddScoped<IUserRepository, UserRepository>();

        // Integration

        return services;
    }
}
