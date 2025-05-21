using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Messages;

namespace DenerViana.Rpc.RabbitMQ.Clients.Api.Application.Cqrs.Events;

public class ClientAddedEvent : Event
{
    public Guid Id { get; private set; }
    public string Origin { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string TaxNumber { get; private set; }
    public string CorrelationId { get; private set; }
    public string CreatedId { get; private set; }
    public string CreatedBy { get; private set; }

    public ClientAddedEvent(Guid id, string origin, string name, string email, string taxNumber, string correlationId, string createdId, string createdBy)
    {
        Id = id;
        AggregateId = id;
        Origin = origin;
        Name = name;
        Email = email;
        TaxNumber = taxNumber;
        CorrelationId = correlationId;
        CreatedId = createdId;
        CreatedBy = createdBy;
    }

}
