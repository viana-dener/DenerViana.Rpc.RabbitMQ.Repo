namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

public class AddressRequest
{
    public Guid ClientId { get;  set; }
    public string Street { get;  set; }
    public string Neighborhood { get;  set; }
    public string City { get;  set; }
    public string Region { get;  set; }
    public string Country { get;  set; }
    public string PostalCode { get;  set; }
}
