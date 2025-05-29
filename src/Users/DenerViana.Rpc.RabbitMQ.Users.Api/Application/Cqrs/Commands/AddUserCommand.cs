using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Validations;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands;

public class AddUserCommand : Command
{
    #region Properties

    public string Origin { get; private set; }
    public string Name { get; private set; }
    public string TaxNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public string CorrelationId { get; private set; }
    public string CreatedId { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }

    #endregion

    #region Builders

    public AddUserCommand(string origin, string name, string taxNumber, string email, string password, string correlationId, string createdId, string createdBy)
    {
        Origin = origin;
        Name = name;
        Email = email;
        Password = password;
        TaxNumber = taxNumber;

        CorrelationId = correlationId;
        CreatedId = createdId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    #endregion

    #region Public Methods

    public override bool IsValid()
    {
        ValidationResult = new AddUserValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    #endregion
}
