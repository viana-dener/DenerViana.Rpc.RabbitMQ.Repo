namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

/// <summary>
/// Represents basic user information used in responses.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the user as a string.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; }
}
