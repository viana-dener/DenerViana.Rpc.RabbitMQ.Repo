using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Configuration;

/// <summary>
/// Classe responsável pela configuração e inicialização dos serviços de Health Checks e Health Checks UI.
/// </summary>
public static class HealthChecksSetup
{
    /// <summary>
    /// Configura os serviços de Health Checks e Health Checks UI, adicionando verificações de saúde para a aplicação e banco de dados.
    /// </summary>
    public static IServiceCollection AddHealthChecksSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddSqlServer(configuration.GetConnectionString("SqlServerConnection"), name: "sqlserver", tags: new[] { "db", "sql" });

        return services;
    }

    /// <summary>
    /// Configura o mapeamento dos endpoints de Health Checks e Health Checks UI na aplicação.
    /// </summary>
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
