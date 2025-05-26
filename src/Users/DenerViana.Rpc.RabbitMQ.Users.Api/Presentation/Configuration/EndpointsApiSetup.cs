using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Middlewares;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Data.Context;
using DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Endpoints;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Configuration;

/// <summary>
/// Provides extension methods for configuring API endpoints, middleware, and related services.
/// </summary>
public static class EndpointsApiSetup
{
    /// <summary>
    /// Configures services required for API endpoints, including DbContext, controllers,
    /// JSON serialization, response compression, and app settings binding.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application configuration instance.</param>
    /// <returns>The updated IServiceCollection with registered services.</returns>
    public static IServiceCollection AddEndpointsApiSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<SqlServerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"))
        );

        services.AddScoped<SqlServerDbContext>();
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
            });

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
    /// Configures the HTTP request pipeline with middleware for Swagger, logging, exception handling,
    /// response compression, HTTPS redirection, and endpoint mapping.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
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
        app.MapUserEndpoint();
    }
}
