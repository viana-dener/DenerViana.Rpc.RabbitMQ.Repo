using DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;
using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;

public interface IDynamicFilter
{
    Expression<Func<TType, bool>> FromFiltroItemList<TType>(IReadOnlyList<FilterItem> filtroItems);
}
