using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Endipoints;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Helpers;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Notifications;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.AutoMapper;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Commands;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Services;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Domain.Services;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Context;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Repository;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Validations;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.IoC;

public static class BootStrapper
{
    public static IServiceCollection AddIocSetup(this IServiceCollection services)
    {
        // Infraestructure
        services.AddScoped<IMainEndpoints, MainEndpoints>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddTransient<IValidator<ClientRequest>, ClientRouteValidator>();

        // Applications
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IClientAppServices, ClientAppServices>();
        services.AddScoped<IRequestHandler<AddClientCommand, ValidationResult>, ClientCommandHandler>();

        // Domain
        services.AddScoped<INotify, Notify>();
        services.AddScoped<ILogInformation, LogInformation>();
        services.AddScoped<IClientServices, ClientServices>();

        // Repository
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped<IClientRepository, ClientRepository>();

        // Integration

        return services;
    }
}