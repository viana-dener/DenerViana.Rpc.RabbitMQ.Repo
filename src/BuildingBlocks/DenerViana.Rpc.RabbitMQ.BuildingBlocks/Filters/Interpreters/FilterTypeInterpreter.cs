using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interpreters;

public abstract class FilterTypeInterpreter<TType>(FilterItem filterItem) : IFilterTypeInterpreter<TType>
{
    private FilterItem _filterItem = filterItem;

    public Expression<Func<TType, bool>> Interpret()
    {
        object value;
        var dynamicType = typeof(TType);
        var parameter = Expression.Parameter(dynamicType, dynamicType.Name.First().ToString());
        var property = Expression.Property(parameter, _filterItem.Property);
        var propertyInfo = (PropertyInfo)property.Member;
        if (propertyInfo.PropertyType == typeof(Guid))
            value = Guid.Parse(_filterItem.Value.ToString());
        else
            value = Convert.ChangeType(_filterItem.Value.ToString(), propertyInfo.PropertyType);
        var constant = Expression.Constant(value);
        var expression = CreateExpression(property, constant);

        return Expression.Lambda<Func<TType, bool>>(expression, parameter);
    }

    internal abstract Expression CreateExpression(MemberExpression property, ConstantExpression constant);
}
