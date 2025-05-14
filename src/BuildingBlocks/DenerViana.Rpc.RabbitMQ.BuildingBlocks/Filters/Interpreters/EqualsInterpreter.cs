using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

public class EqualsInterpreter<TType>(FilterItem filterItem) : FilterTypeInterpreter<TType>(filterItem)
{
    internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        => Expression.Equal(property, constant);
}
