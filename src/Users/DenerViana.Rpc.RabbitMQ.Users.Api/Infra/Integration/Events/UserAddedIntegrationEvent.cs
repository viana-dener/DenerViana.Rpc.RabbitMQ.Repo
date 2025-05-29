using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Cqrs.Integration.Events;

namespace DenerViana.Rpc.RabbitMQ.Users.Api.Infra.Integration.Events
{
    public class UserAddedIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }

        public string Origin { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string TaxNumber { get; private set; }

        public UserAddedIntegrationEvent(Guid id, string origin, string name, string email, string taxNumber)
        {
            Id = id;
            Origin = origin;
            Name = name;
            Email = email;
            TaxNumber = taxNumber;
        }

    }
}
