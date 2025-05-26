namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

/// <summary>
/// Represents detailed address information associated with a client.
/// </summary>
public class AddressDetailsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the client.
    /// </summary>
    public Guid ClientId { get; set; }

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
    /// Gets or sets the region or state of the address.
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the postal code or ZIP code of the address.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the address.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the address.
    /// </summary>
    public double? Longitude { get; set; }
}
