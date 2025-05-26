namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents the detailed response model for an address, including geographic coordinates.
/// </summary>
public class AddressDetailsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the client associated with the address.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the street name and number of the address.
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the neighborhood or district of the address.
    /// </summary>
    public string Neighborhood { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the region, state, or province of the address.
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the country of the address.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Gets or sets the postal or ZIP code of the address.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the address, if available.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the address, if available.
    /// </summary>
    public double? Longitude { get; set; }
}
