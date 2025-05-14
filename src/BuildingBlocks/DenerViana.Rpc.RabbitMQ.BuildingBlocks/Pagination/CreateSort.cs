using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Pagination;

public static class CreateSort
{
    public static Expression<Func<TEntity, object>> SortBy<TEntity>(string orderBy)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        Expression property = parameter;

        if (orderBy.Contains('.'))
        {
            foreach (var item in orderBy.Split('.'))
            {
                property = Expression.PropertyOrField(property, item);
            }
        }
        else
        {
            property = Expression.Property(parameter, orderBy);
        }

        var conversion = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<TEntity, object>>(conversion, parameter);
        return lambda;
    }
}
