namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents basic address information used in responses.
/// </summary>
public class AddressResponse
{
    /// <summary>
    /// Gets or sets the street name of the address.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the neighborhood of the address.
    /// </summary>
    public string Neighborhood { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the postal code or ZIP code of the address.
    /// </summary>
    public string PostalCode { get; set; }
}
