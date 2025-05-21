namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

/// <summary>
/// Represents a request to create or update a client.
/// </summary>
public class ClientRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Origin { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string TaxNumber { get; set; }
}
