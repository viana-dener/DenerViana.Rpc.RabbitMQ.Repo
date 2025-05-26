namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Request;

/// <summary>
/// Represents a request model for client information.
/// </summary>
public class ClientRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the client.
    /// Default value is a new Guid generated on instantiation.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the origin or source of the client.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the client.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the tax identification number of the client.
    /// </summary>
    public string TaxNumber { get; set; }
}
