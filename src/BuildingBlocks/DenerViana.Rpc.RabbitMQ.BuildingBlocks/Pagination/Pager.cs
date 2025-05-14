namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Pagination;

public class Pager : Order
{
    private const int _maxItemsPerPage = 50;
    private const int _minItemsPerPage = 10;
    private int page;
    private int itemsPerPage;

    public int Page
    {
        get { return page <= 0 ? 1 : page; }
        set { page = value; }
    }
    public int ItemsPerPage
    {
        get { return itemsPerPage <= 0 ? _minItemsPerPage : itemsPerPage > _maxItemsPerPage ? _maxItemsPerPage : itemsPerPage; }
        set { itemsPerPage = value; }
    }

    public static int MaxItemsPerPage() => _maxItemsPerPage;
    public static int MinItemsPerPage() => _minItemsPerPage;
}
