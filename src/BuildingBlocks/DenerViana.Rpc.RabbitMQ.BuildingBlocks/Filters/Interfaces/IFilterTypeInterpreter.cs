using System.Linq.Expressions;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Interfaces;

public interface IFilterTypeInterpreter<TType>
{
    Expression<Func<TType, bool>> Interpret();
}
