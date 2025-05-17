using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Application.Cqrs.Commands;

public class AddUserCommand : Command
{
    public string Origin { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Password { get; private set; }
    public string CreatedId { get; private set; }
    public string CreatedBy { get; private set; }
    public AddUserCommand(string origin, string name, Email email, string password, string createdId, string createdBy)
    {
        Origin = origin;
        Name = name;
        Email = email;
        Password = password;
        CreatedId = createdId;
        CreatedBy = createdBy;
    }
}
