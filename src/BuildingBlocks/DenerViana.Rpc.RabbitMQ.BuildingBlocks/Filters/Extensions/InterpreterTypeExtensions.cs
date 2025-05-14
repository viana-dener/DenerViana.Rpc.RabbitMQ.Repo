using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Extensions;

public static class InterpreterTypeExtensions
{
    public static IFilterTypeInterpreter<TType> And<TType>(this IFilterTypeInterpreter<TType> left, IFilterTypeInterpreter<TType> right)
        => new AndInterpreter<TType>(left, right);
}
