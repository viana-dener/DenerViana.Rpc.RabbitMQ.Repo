namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

public class ClientRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Origin { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string TaxNumber { get; set; }
}
