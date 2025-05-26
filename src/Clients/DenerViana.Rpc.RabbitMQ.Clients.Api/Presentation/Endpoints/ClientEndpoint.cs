using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Mediator;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Models.Response;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Commands;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Endpoints;

/// <summary>
/// Defines API endpoints for client operations.
/// </summary>
public static class ClientEndpoint
{
    #region Public Methods

    /// <summary>
    /// Maps client-related endpoints in the web application.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    public static void MapClientEndpoint(this WebApplication app)
    {
        app.MapGet("clients", async (IMainEndpoints endpoint, IClientAppServices ClientApp) =>
        {
            var result = await ClientApp.GetAllAsync();
            return endpoint.CustomResponse(result);
        })
        .WithName("GetClients")
        .WithOpenApi()
        .Produces<IEnumerable<ClientResponse>>(200)
        .Produces<ErrorResponse>(400)
        .WithMetadata(new SwaggerOperationAttribute("Get all clients")
        {
            OperationId = "GetClients",
            Tags = new[] { "Clients" }
        })
        .WithMetadata(new ResponseCacheAttribute
        {
            Duration = 60,
            Location = ResponseCacheLocation.Any
        });

        app.MapPost("clients", async (INotify _notify, IMainEndpoints _endpoint, IMediatorHandler _handler, ClientRequest client) =>
        {
            var headers = _endpoint.HttpContext.Request.HttpContext.Request.Headers;
            var requiredHeaders = new Dictionary<string, bool>
            {
                { "x-origin", true },
                { "x-user-id", true },
                { "x-user-name", true },
                { "x-user-business-area", false },
                { "x-correlation-id", true }
            };

            // Validate headers
            if (!HeadersValidate(_notify, headers, requiredHeaders)) return _endpoint.CustomResponse();

            var userInfo = CreateUserInfo(headers);
            var result = await _handler.SendCommand(new AddClientCommand(client.Id, userInfo.Origin, client.Name, client.Email, client.TaxNumber, userInfo.CorrelationId, userInfo.UserId, userInfo.UserName));

            return _endpoint.CustomResponse(result.IsValid);
        })
        .AddEndpointFilter<ValidationFilter<ClientRequest>>()
        .WithName("PostClient")
        .WithOpenApi()
        .Produces<GenericResponse>(200)
        .Produces<ErrorResponse>(400)
        .WithMetadata(new SwaggerOperationAttribute("Add a new Client")
        {
            OperationId = "PostClient",
            Tags = new[] { "Clients" }
        })
        .WithMetadata(new ResponseCacheAttribute
        {
            Duration = 60,
            Location = ResponseCacheLocation.Any
        });
    }

    #endregion

    #region Private Methods

    private static UserInfoDto CreateUserInfo(IHeaderDictionary headers)
    {
        return new UserInfoDto()
        {
            Origin = headers["x-origin"].ToString(),
            UserId = headers["x-user-id"].ToString(),
            UserName = headers["x-user-name"].ToString(),
            UserBusinessArea = headers.TryGetValue("x-user-business-area", out var businessArea) ? businessArea.ToString() : null,
            CorrelationId = headers["x-correlation-id"].ToString()
        };
    }

    private static bool HeadersValidate(INotify notify, IHeaderDictionary headers, Dictionary<string, bool> requiredHeaders)
    {
        var headerValidate = ExtendedMethods.ValidateHeaders(headers, requiredHeaders);
        if (!headerValidate.Result)
        {
            foreach (var item in headerValidate.Errors)
            {
                notify.AddError($"{item.Key}, {item.Value}");
            }
        }

        return headerValidate.Result;
    }

    #endregion
}
