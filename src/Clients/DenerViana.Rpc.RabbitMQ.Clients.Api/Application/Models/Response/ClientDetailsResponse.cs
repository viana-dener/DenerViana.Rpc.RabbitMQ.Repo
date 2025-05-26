using DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents detailed information about a client, including contact, identification, and address data.
/// </summary>
public class ClientDetailsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the client.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the client.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email information of the client.
    /// </summary>
    public Email Email { get; set; }

    /// <summary>
    /// Gets or sets the tax identification number of the client.
    /// </summary>
    public TaxNumber TaxNumber { get; set; }

    /// <summary>
    /// Gets or sets the client's picture, typically a URL or base64 string.
    /// </summary>
    public string Picture { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client is excluded (soft-deleted).
    /// </summary>
    public bool IsExcluded { get; set; }

    /// <summary>
    /// Gets or sets the detailed address information of the client.
    /// </summary>
    public AddressDetailsResponse Address { get; set; }
}
