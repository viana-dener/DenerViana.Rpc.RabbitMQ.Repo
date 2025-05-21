using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Configures Swagger documentation for the API.
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Adds and configures Swagger services in the application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(s =>
        {
            var provider = services.BuildServiceProvider();
            var settings = provider.GetRequiredService<IOptions<SwaggerSettings>>().Value;

            s.EnableAnnotations();
            s.SwaggerDoc(settings.Version, new OpenApiInfo
            {
                Title = settings.Title,
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name = settings.Contact.Name,
                    Email = settings.Contact.Email,
                    Url = new Uri(settings.Contact.Url)
                },
                License = new OpenApiLicense
                {
                    Name = settings.License.Name,
                    Url = new Uri(settings.License.Url)
                },
                Version = settings.Version
            });

            var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
            if (!string.IsNullOrWhiteSpace(xfile) && !string.IsNullOrWhiteSpace(xpath))
                s.IncludeXmlComments(xpath);
        });

        return services;
    }
}
