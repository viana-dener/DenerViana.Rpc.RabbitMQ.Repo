using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Models.Response;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Net;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Endipoints;

/// <summary>
/// Classe concreta que implementa recursos para os Endpoints
/// </summary>
public class MainEndpoints(ILogger<MainEndpoints> logger, IHttpContextAccessor httpContextAccessor, INotify notify) : IMainEndpoints
{
    private readonly ILogger<MainEndpoints> _logger = logger;
    private readonly INotify _notify = notify;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// retorna o HttpContext
    /// </summary>
    public HttpContext HttpContext => _httpContextAccessor.HttpContext;

    /// <summary>
    /// Retorna um response customizado
    /// </summary>
    public IResult CustomResponse(object result = null)
    {
        if (!IsValid())
            return IsNotValid();

        if (result is bool value)
            return Results.Ok(new GenericResponse { Result = value });

        return Results.Ok(result);
    }

    public IResult CustomResponse(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                _notify.AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        return CustomResponse(new GenericResponse { Result = true });
    }

    /// <summary>
    /// Retorna um response customizado vindo na Model State
    /// </summary>
    public IResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(x => x.Errors);

        foreach (var error in errors)
        {
            _notify.AddError(error.ErrorMessage);
        }

        return CustomResponse();
    }

    /// <summary>
    /// Verifica se é válido
    /// </summary>
    public bool IsValid()
    {
        return !_notify.GetErrors().Any();
    }

    private IResult IsNotValid()
    {
        var requestLog = _httpContextAccessor?.HttpContext?.Items["RequestLogDto"] as RequestLogDto;

        using (LogContext.PushProperty("CorrelationId", requestLog.CorrelationId))
        using (LogContext.PushProperty("RequestMethod", requestLog.Method))
        using (LogContext.PushProperty("StatusCode", (HttpStatusCode)_notify.GetStatusCode()))
        using (LogContext.PushProperty("Headers", requestLog.Headers))
        using (LogContext.PushProperty("Payload", requestLog.Payload))
        {
            _logger.LogError(string.Join(" | ", _notify.GetErrors()));

            return Results.BadRequest(new ErrorResponse
            {
                Errors = [.. _notify.GetErrors()]
            });
        }
    }
}
