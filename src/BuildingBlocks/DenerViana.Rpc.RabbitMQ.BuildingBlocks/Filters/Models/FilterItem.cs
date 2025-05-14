namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Filters.Models;

public struct FilterItem
{
    public string Property { get; set; }
    public string FilterType { get; set; }
    public object Value { get; set; }
}
