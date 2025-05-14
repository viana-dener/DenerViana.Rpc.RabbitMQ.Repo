using Microsoft.AspNetCore.Mvc.Filters;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Interfaces;

public interface IFilterInterpreterFactory
{
    IFilterTypeInterpreter<TType> Create<TType>(FilterItem filterItem);
}
