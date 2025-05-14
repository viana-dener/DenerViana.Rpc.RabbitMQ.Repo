using System.ComponentModel;

namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Enumerators;

public enum FilterType
{
    [Description("Equals")]
    Equals,
    [Description("Contains")]
    Contains,
    [Description("Start With")]
    StartWith,
    [Description("Less Than")]
    LessThan,
    [Description("GreaterThan")]
    GreaterThan
}
