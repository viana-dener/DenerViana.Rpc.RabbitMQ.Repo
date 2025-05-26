using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Models.Response;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Interfaces;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Presentation.Endpoints;

/// <summary>
/// Defines HTTP endpoints related to User operations, including retrieving all users and adding a new user.
/// Handles request validation, header extraction, and integrates with application services for business logic.
/// </summary>
public static class UserEndpoint
{
    /// <summary>
    /// Maps user-related endpoints (GET and POST) to the application's routing pipeline.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to add the endpoints to.</param>
    public static void MapUserEndpoint(this WebApplication app)
    {
        // GET /users - Retrieves all users.
        app.MapGet("users", async (IMainEndpoints endpoint, IUserAppServices userApp) =>
        {
            var result = await userApp.GetAllAsync();
            return endpoint.CustomResponse(result);
        })
        .WithName("GetUsers")
        .WithOpenApi()
        .Produces<IEnumerable<UserResponse>>(200)
        .Produces<ErrorResponse>(400)
        .WithMetadata(new SwaggerOperationAttribute("Get all users")
        {
            OperationId = "GetUsers",
            Tags = new[] { "Users" }
        })
        .WithMetadata(new ResponseCacheAttribute
        {
            Duration = 60,
            Location = ResponseCacheLocation.Any
        });

        // POST /users - Adds a new user after validating headers and request body.
        app.MapPost("users", async (INotify notify, IMainEndpoints endpoint, IUserAppServices userApp, UserRequest User) =>
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

            var userInfo = CreateUserInfo(headers);

            var result = await userApp.AddAsync(User, userInfo);

            return endpoint.CustomResponse(result);

        })
        .AddEndpointFilter<ValidationFilter<UserRequest>>()
        .WithName("PostUser")
        .WithOpenApi()
        .Produces<GenericResponse>(200)
        .Produces<ErrorResponse>(400)
        .WithMetadata(new SwaggerOperationAttribute("Add a new user")
        {
            OperationId = "PostUser",
            Tags = new[] { "Users" }
        })
        .WithMetadata(new ResponseCacheAttribute
        {
            Duration = 60,
            Location = ResponseCacheLocation.Any
        });
    }

    /// <summary>
    /// Extracts user information from HTTP headers.
    /// </summary>
    private static UserInfoDto CreateUserInfo(IHeaderDictionary headers)
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

    /// <summary>
    /// Validates required HTTP headers and reports errors via the notification service.
    /// </summary>
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
}
