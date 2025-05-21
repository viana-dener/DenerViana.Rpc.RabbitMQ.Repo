using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Configures health checks for monitoring the application's status.
/// </summary>
public static class HealthChecksSetup
{
    /// <summary>
    /// Adds health check services to the application, including MongoDB monitoring.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddHealthChecksSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddMongoDb(configuration.GetConnectionString("MongoDbConnection"), name: "mongodb", tags: new[] { "db", "nosql" });

        return services;
    }

    /// <summary>
    /// Configures the health check endpoint for external monitoring.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    public static void UseHealthChecksConfiguration(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = report.Status.ToString(),
                    results = report.Entries.Select(e => new
                    {
                        key = e.Key,
                        status = e.Value.Status.ToString(),
                        description = e.Value.Description
                    })
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        });
    }
}
