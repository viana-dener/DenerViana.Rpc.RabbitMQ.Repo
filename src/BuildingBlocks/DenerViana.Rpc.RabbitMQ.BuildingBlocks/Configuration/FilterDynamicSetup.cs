using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Configuration;

public static class FilterDynamicSetup
{
    public static IServiceCollection AddFilterDynamic(this IServiceCollection services)
    {
        services.AddSingleton<IFilterInterpreterFactory, FilterInterpreterFactory>();
        services.AddSingleton<IDynamicFilter, DynamicFilter>();

        return services;
    }
}
