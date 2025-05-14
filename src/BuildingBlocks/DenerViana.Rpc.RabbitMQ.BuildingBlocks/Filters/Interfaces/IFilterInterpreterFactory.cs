using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;

public interface IFilterInterpreterFactory
{
    IFilterTypeInterpreter<TType> Create<TType>(FilterItem filterItem);
}
