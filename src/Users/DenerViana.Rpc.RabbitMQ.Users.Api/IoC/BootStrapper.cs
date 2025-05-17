using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Endipoints;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Helpers;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Notifications;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.AutoMapper;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Services;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Services;
using DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Repository;
using DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Validations;
using FluentValidation;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.IoC;

public static class BootStrapper
{
    public static IServiceCollection AddIocSetup(this IServiceCollection services)
    {
        // Infraestructure
        services.AddScoped<IMainEndpoints, MainEndpoints>();
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddTransient<IValidator<UserRequest>, UserRouteValidator>();

        // Applications
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IUserAppServices, UserAppServices>();

        // Domain
        services.AddScoped<INotify, Notify>();
        services.AddScoped<ILogInformation, LogInformation>();
        services.AddScoped<IUserServices, UserServices>();

        // Repository
        services.AddScoped<IUserRepository, UserRepository>();

        // Integration

        return services;
    }
}
