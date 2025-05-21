namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents a response containing basic client information.
/// </summary>
public class ClientResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
