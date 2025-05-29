using DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Domain.Entities;

/// <summary>
/// Represents a user aggregate root entity with identity, contact information, and status.
/// </summary>
public class User : EntityDb, IAggregateRoot
{
    #region Properties

    /// <summary>
    /// Gets the origin or source of the user.
    /// </summary>
    public string Origin { get; private set; }

    /// <summary>
    /// Gets the full name of the user.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the email value object of the user.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the user's password.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user is excluded (soft deleted).
    /// </summary>
    public bool IsExcluded { get; private set; }

    #endregion

    #region Builders

    /// <summary>
    /// Parameterless constructor for entity framework and serialization.
    /// </summary>
    public User() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class with specified details.
    /// </summary>
    /// <param name="origin">The origin or source of the user.</param>
    /// <param name="name">The full name of the user.</param>
    /// <param name="email">The email address of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <param name="createdId">Identifier of the user who created this user.</param>
    /// <param name="createdBy">Name of the user who created this user.</param>
    public User(string origin, string name, string email, string password, string createdId, string createdBy, Guid? correlationId)
    {
        Origin = origin;
        Name = name;
        Email = new Email(email);
        Password = password;
        IsExcluded = false;
        CreatedId = createdId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
        CorrelationId = correlationId;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Marks the user as excluded (soft delete).
    /// </summary>
    public void Exclude() => IsExcluded = true;

    #endregion
}
