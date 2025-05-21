namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents detailed address information associated with a client.
/// </summary>
public class AddressDetailsResponse
{
    public Guid ClientId { get; set; }
    public string Street { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
