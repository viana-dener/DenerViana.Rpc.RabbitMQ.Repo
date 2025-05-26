namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

/// <summary>
/// Represents a request model for an address associated with a client.
/// </summary>
public class AddressRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the client.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the street name.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the neighborhood.
    /// </summary>
    public string Neighborhood { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the region, state, or province.
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the postal or ZIP code.
    /// </summary>
    public string PostalCode { get; set; }
}

