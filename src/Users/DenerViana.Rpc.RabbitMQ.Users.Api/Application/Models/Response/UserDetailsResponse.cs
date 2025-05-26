namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Models.Response;

/// <summary>
/// Represents detailed information about a user, including identification, contact, and address details.
/// </summary>
public class UserDetailsResponse
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// Gets the full name of the user.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the user's password (typically hashed or encrypted).
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets the tax identification number of the user.
    /// </summary>
    public string TaxNumber { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user is excluded (soft-deleted).
    /// </summary>
    public bool IsExcluded { get; private set; }

    /// <summary>
    /// Gets the detailed address information of the user.
    /// </summary>
    public AddressDetailsResponse Address { get; private set; } = new AddressDetailsResponse();
}
