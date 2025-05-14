using DenerViana.Rpc.RabbitMQ.BuildingBlocks.DomainObjects;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

namespace DenerViana.Rpc.RabbitMQ.Users.Domain.Entities;

/// <summary>
/// 
/// </summary>
public class User : EntityDb, IAggregateRoot
{
    #region Properties

    public string Origin { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public bool IsExcluded { get; private set; }

    #endregion

    #region Builders

    public User() { }
    public User(string origin, string name, string email, string password, string createdBy)
    {
        Origin = origin;
        Name = name;
        Email = new Email(email);
        Password = password;
        IsExcluded = false;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
    }

    #endregion

    #region Public Methods

    public void Exclude() => IsExcluded = true;

    #endregion
}
