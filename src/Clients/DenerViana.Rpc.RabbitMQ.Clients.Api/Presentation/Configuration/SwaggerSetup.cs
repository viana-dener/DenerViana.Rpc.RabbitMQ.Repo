using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;

/// <summary>
/// Classe responsavel por definir configurações do Swagger
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Método de extensão que estende configurações das interfaces IServiceCollection e IConfiguration
    /// </summary>
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
