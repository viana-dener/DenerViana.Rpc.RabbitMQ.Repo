using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Extensions;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;
using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters;

public class DynamicFilter(IFilterInterpreterFactory factory) : IDynamicFilter
{
    private readonly IFilterInterpreterFactory _factory = factory;

    public Expression<Func<TType, bool>> FromFiltroItemList<TType>(IReadOnlyList<FilterItem> filtroItems)
    {
        return filtroItems
            .Select(filtroItem => _factory.Create<TType>(filtroItem))
            .Aggregate((curr, next) => curr.And(next))
            .Interpret();
    }
}
