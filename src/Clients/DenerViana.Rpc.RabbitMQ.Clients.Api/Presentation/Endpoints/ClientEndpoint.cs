using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Models.Response;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Presentation.Endpoints;

public static class ClientEndpoint
{
    #region Public Methods

    /// <summary>
    /// Mapeia os endpoints relacionados ao produtor na aplicação, incluindo operações de listagem e cadastro de contas.
    /// Define as rotas, filtros, metadados do Swagger, cache de resposta e validação de cabeçalhos.
    /// </summary>
    public static void MapClientEndpoint(this WebApplication app)
    {
        app.MapGet("Client", async (IMainEndpoints endpoint, IClientAppServices ClientApp) =>
        {
            var result = await ClientApp.GetAllAsync();

            return endpoint.CustomResponse(result);

        }).WithName("GetClients")
          .WithOpenApi()
          .Produces<IEnumerable<ClientResponse>>(200)
          .Produces<ErrorResponse>(400)
          .WithMetadata(new SwaggerOperationAttribute("Get all clients")
          {
              OperationId = "GetClients",
              Tags = new[] { " Clients" }
          })
          .WithMetadata(new ResponseCacheAttribute
          {
              Duration = 60,
              Location = ResponseCacheLocation.Any
          });

        app.MapPost("Client", async (INotify notify, IMainEndpoints endpoint, IClientAppServices ClientApp, ClientRequest client) =>
        {
            var headers = endpoint.HttpContext.Request.HttpContext.Request.Headers;
            var requiredHeaders = new Dictionary<string, bool>
            {
                { "x-origin", true },
                { "x-user-id", true },
                { "x-user-name", true },
                { "x-user-business-area", false },
                { "x-correlation-id", true }
            };

            if (!HeadersValidate(notify, headers, requiredHeaders)) return endpoint.CustomResponse();

            var clientInfo = CreateClientInfo(headers);

            var result = await ClientApp.AddAsync(client, clientInfo);

            return endpoint.CustomResponse(result);

        }).AddEndpointFilter<ValidationFilter<ClientRequest>>()
          .WithName("PostClient")
          .WithOpenApi()
          .Produces<GenericResponse>(200)
          .Produces<ErrorResponse>(400)
          .WithMetadata(new SwaggerOperationAttribute("Add a new Client")
          {
              OperationId = "PostClient",
              Tags = new[] { " Clients" }
          })
          .WithMetadata(new ResponseCacheAttribute
          {
              Duration = 60,
              Location = ResponseCacheLocation.Any
          });
    }

    #endregion

    #region Private Methods

    private static UserInfoDto CreateClientInfo(IHeaderDictionary headers)
    {
        return new UserInfoDto()
        {
            Origin = headers["x-origin"].ToString(),
            UserId = headers["x-user-id"].ToString(),
            UserName = headers["x-user-name"].ToString(),
            UserBusinessArea = headers.TryGetValue("x-Client-business-area", out var businessArea) ? businessArea.ToString() : null,
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
                notify.AddError(item.Key + ", " + item.Value);
            }
        }

        return headerValidate.Result;
    }

    #endregion
}
