using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Configuration;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Settings;
using DenerViana.Rpc.RabbitMQ.Clients.Api.IoC;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiSetup(builder.Configuration);

var settings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .Enrich.WithProperty("App", "UserAPI")
        .Enrich.WithProperty("v1", "VianaHub.Rpc.RabbitMQ.User")
        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(settings.Elasticsearch))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"vianahub-rpc-rabbitmq-{DateTime.UtcNow:yyyy-MM}",
            ModifyConnectionSettings = conn =>
                conn.BasicAuthentication("elastic", "VianaHub2023")
                    .ServerCertificateValidationCallback((o, cert, chain, errors) => true)
        });
});

builder.Services.AddCorsSetup(builder.Configuration);
builder.Services.AddHealthChecksSetup(builder.Configuration);
builder.Services.AddSwaggerSetup(builder.Configuration);
builder.Services.AddIocSetup();

var app = builder.Build();

app.UseEndpointsApiConfiguration();
app.UseHealthChecksConfiguration();

app.Run();