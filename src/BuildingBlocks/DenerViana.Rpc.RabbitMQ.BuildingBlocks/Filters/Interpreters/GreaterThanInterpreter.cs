using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

public class GreaterThanInterpreter<TType> : FilterTypeInterpreter<TType>
{
    public GreaterThanInterpreter(FilterItem filterItem) : base(filterItem)
    {
    }

    internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        => Expression.GreaterThan(property, constant);
}
