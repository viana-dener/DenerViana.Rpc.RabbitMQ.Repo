namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Models.Response;

/// <summary>
/// Represents basic client information used in responses.
/// </summary>
public class ClientResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the client as a string.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the client.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the client.
    /// </summary>
    public string Email { get; set; }
}
