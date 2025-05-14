namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Pagination;

public class Order
{
    private string orderBy;
    private string orderType;

    public string OrderBy
    {
        get { return !string.IsNullOrWhiteSpace(orderBy) ? orderBy : "Id"; }
        set { orderBy = value; }
    }
    public string OrderType
    {
        get { return !string.IsNullOrWhiteSpace(orderType) && (orderType.ToLower().Equals("asc") || orderType.ToLower().Equals("desc")) ? orderType : "asc"; }
        set { orderType = value; }
    }
}
