using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Endipoints;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Middlewares;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Infra.Context;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Endpoints;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Validations;
using FluentValidation;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Classe responsavel por definir configurações de pipeline da API
/// </summary>
public static class EndpointsApiSetup
{
    /// <summary>
    /// Método de extensão que estende configurações das interfaces IServiceCollection e IConfiguration
    /// </summary>
    public static IServiceCollection AddEndpointsApiSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IMainEndpoints, MainEndpoints>();
        services.AddTransient<IValidator<ClientRequest>, ClientRouteValidator>();
        
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
    /// Método de extensão que estende configurações da WebApplication
    /// </summary>
    public static void UseEndpointsApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
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
