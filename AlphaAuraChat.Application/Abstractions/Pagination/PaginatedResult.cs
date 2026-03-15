namespace AlphaAuraChat.Application.Abstractions.Pagination;

public record PaginatedResult<TValue>
{
    public IReadOnlyList<TValue> Items { get; init; } = default!;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; init; }
}
