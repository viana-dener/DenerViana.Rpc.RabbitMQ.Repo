using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Middlewares;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Context;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Endpoints;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Configures API-related services and middleware for the application.
/// </summary>
public static class EndpointsApiSetup
{
    /// <summary>
    /// Adds necessary configurations and services for the API endpoints.
    /// </summary>
    /// <param name="services">The service collection to which dependencies will be added.</param>
    /// <param name="configuration">The configuration settings for the application.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddEndpointsApiSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        MongoSetup.AddMongoDbConvention();
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddSingleton<MongoDbContext>();

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = System.IO.Compression.CompressionLevel.Fastest;
        });

        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        services.Configure<SwaggerSettings>(configuration.GetSection("SwaggerSettings"));

        return services;
    }

    /// <summary>
    /// Configures middleware and API behavior for request handling.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    public static void UseEndpointsApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RPC RabbitMQ");
                c.DocumentTitle = "RPC RabbitMQ";
            });
        }

        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
                diagnosticContext.Set("CorrelationId", httpContext.Request.Headers["x-correlation-id"].ToString());
            };
        });

        app.UseMiddleware<RequestContextMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseResponseCompression();
        app.UseHttpsRedirection();

        app.MapClientEndpoint();
    }
}
