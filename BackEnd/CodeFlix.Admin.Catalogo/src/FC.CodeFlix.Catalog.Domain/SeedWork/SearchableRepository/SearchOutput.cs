namespace FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

public class SearchOutput<TAggregate> where TAggregate : AggregateRoot
{
    public int CurrentPage { get; set; }
    public int PerPages { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TAggregate> Items { get; set; }

    public SearchOutput(int currentPage, int perPages, int total, IReadOnlyList<TAggregate> items)
    {
        CurrentPage = currentPage;
        PerPages = perPages;
        Total = total;
        Items = items;
    }
}