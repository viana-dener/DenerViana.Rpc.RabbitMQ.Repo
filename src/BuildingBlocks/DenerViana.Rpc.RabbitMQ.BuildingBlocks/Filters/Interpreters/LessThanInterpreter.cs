using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

public class LessThanInterpreter<TType> : FilterTypeInterpreter<TType>
{
    public LessThanInterpreter(FilterItem filterItem) : base(filterItem)
    {
    }

    internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        => Expression.LessThan(property, constant);
}
