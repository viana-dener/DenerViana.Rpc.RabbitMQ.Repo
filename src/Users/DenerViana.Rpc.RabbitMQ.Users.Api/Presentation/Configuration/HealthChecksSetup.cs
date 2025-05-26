using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Configuration;

/// <summary>
/// Provides extension methods for configuring health checks services and middleware.
/// </summary>
public static class HealthChecksSetup
{
    /// <summary>
    /// Adds health check services to the dependency injection container,
    /// including a self-check and SQL Server database health check.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the health checks to.</param>
    /// <param name="configuration">The application configuration instance to retrieve connection strings.</param>
    /// <returns>The updated IServiceCollection with health checks registered.</returns>
    public static IServiceCollection AddHealthChecksSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddSqlServer(configuration.GetConnectionString("SqlServerConnection"), name: "sqlserver", tags: new[] { "db", "sql" });

        return services;
    }

    /// <summary>
    /// Configures the application to use health check endpoints with a custom JSON response writer.
    /// The health endpoint responds with overall status and details for each health check registered.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
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
                    results = report.Entries.Select(e => new {
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
