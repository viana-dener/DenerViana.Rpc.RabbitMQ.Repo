namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

/// <summary>
/// Represents a request to add or update an address associated with a client.
/// </summary>
public class AddressRequest
{
    public Guid ClientId { get; set; }
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}
