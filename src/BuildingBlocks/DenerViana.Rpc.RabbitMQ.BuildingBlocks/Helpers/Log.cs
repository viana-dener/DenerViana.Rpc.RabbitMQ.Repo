using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Dtos;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Net;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Helpers;

/// <summary>
/// Serviço responsável por publicar logs de informação enriquecidos com dados de contexto da requisição HTTP,
/// como CorrelationId, status HTTP, mensagem de erro e payload da requisição, se disponível.
/// </summary>
public class Log(ILogger<Log> logger, IHttpContextAccessor httpContextAccessor) : ILog
{
    private readonly ILogger<Log> _logger = logger;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Publica um log do tipo Information utilizando propriedades da requisição atual, 
    /// como CorrelationId, código de status, mensagem e payload, se carregado previamente no contexto.
    /// </summary>
    /// <param name="message">Mensagem a ser registrada no log.</param>
    public void Publish(LogLevel level, string message)
    {
        var requestLog = _httpContextAccessor?.HttpContext?.Items["RequestLogDto"] as RequestLogDto;

        using (LogContext.PushProperty("CorrelationId", requestLog.CorrelationId))
        using (LogContext.PushProperty("RequestMethod", requestLog.Method))
        using (LogContext.PushProperty("StatusCode", HttpStatusCode.OK))
        using (LogContext.PushProperty("Headers", requestLog.Headers))
        using (LogContext.PushProperty("Payload", requestLog.Payload))
        {
            _logger.Log(level, message);
        }
    }
}
