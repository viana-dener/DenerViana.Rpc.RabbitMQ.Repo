namespace DenerViana.Rpc.RabbitMQ.BuildingBlocks.Pagination;

public class ListPage<T>
{

    public IEnumerable<T> Items { get; set; }
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }

    public ListPage() { }
    public ListPage(IEnumerable<T> items, int page, int itemsPerPage, int totalItems, int totalPages)
    {
        Items = items;
        Page = page;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }
}
