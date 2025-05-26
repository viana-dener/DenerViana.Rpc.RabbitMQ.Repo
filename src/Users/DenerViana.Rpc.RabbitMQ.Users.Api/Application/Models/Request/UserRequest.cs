namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Request;

/// <summary>
/// Represents the data required to create or register a user.
/// </summary>
public class UserRequest
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the user's tax identification number.
    /// </summary>
    public string TaxNumber { get; set; }

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the user's chosen password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation of the user's password.
    /// </summary>
    public string ConfirmPassword { get; set; }
}
