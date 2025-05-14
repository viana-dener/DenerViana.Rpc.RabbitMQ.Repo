using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

public class ContainsInterpreter<TType>(FilterItem filterItem) : FilterTypeInterpreter<TType>(filterItem)
{
    internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
    {
        var method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        return Expression.Call(property, method, constant);
    }
}
