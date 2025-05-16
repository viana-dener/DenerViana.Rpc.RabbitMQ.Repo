using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Models.Response;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Users.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DenerViana.Rpc.RabbitMQ.Users.Presentation.Endpoints;

public static class UserEndpoint
{
    #region Public Methods

    /// <summary>
    /// Mapeia os endpoints relacionados ao produtor na aplicação, incluindo operações de listagem e cadastro de contas.
    /// Define as rotas, filtros, metadados do Swagger, cache de resposta e validação de cabeçalhos.
    /// </summary>
    public static void MapUserEndpoint(this WebApplication app)
    {
        app.MapGet("User", async (IMainEndpoints endpoint, IUserAppServices userApp) =>
        {
            var result = await userApp.GetAllAsync();

            return endpoint.CustomResponse(result);

        }).WithName("GetUsers")
          .WithOpenApi()
          .Produces<IEnumerable<UserResponse>>(200)
          .Produces<ErrorResponse>(400)
          .WithMetadata(new SwaggerOperationAttribute("Get all users")
          {
              OperationId = "GetUsers",
              Tags = new[] { " Users" }
          })
          .WithMetadata(new ResponseCacheAttribute
          {
              Duration = 60,
              Location = ResponseCacheLocation.Any
          });

        app.MapPost("User", async (INotify notify, IMainEndpoints endpoint, IUserAppServices userApp, RegisterUserRequest User) =>
        {
            var headers = endpoint.HttpContext.Request.HttpContext.Request.Headers;
            var requiredHeaders = new Dictionary<string, bool>
            {
                { "x-origin", true },
                { "x-user-id", true },
                { "x-user-business-area", false },
                { "x-correlation-id", true }
            };

            if (!HeadersValidate(notify, headers, requiredHeaders)) return endpoint.CustomResponse();

            var userInfo = CreateUserInfo(headers);

            var result = await userApp.RegisterUserAsync(User, userInfo);

            return endpoint.CustomResponse(result);

        }).AddEndpointFilter<ValidationFilter<RegisterUserRequest>>()
          .WithName("PostUser")
          .WithOpenApi()
          .Produces<GenericResponse>(200)
          .Produces<ErrorResponse>(400)
          .WithMetadata(new SwaggerOperationAttribute("Register a new user")
          {
              OperationId = "PostUser",
              Tags = new[] { " Users" }
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
                notify.AddError(item.Key + ", " + item.Value);
            }
        }

        return headerValidate.Result;
    }

    #endregion
}
