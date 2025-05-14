using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Extensions;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Tools;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.ValueObjects;

public class TaxNumber
{
    public string Number { get; private set; }

    // EF - Relational
    protected TaxNumber() { }
    public TaxNumber(string number)
    {
        if (!number.IsTaxNumberValid()) throw new DomainException("The TaxNumber is invalid!");

        Number = number;
    }
}
