using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Extensions;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

public class Email
{
    public string Address { get; private set; }

    // EF - Relational
    protected Email() { }
    public Email(string address)
    {
        if (!address.IsEmailValid()) throw new DomainException("The Email is invalid!");

        Address = address;
    }
}
