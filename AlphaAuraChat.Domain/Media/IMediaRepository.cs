namespace AlphaAuraChat.Domain.Media;

public interface IMediaRepository
{
    public Task AddRangeAsync(IEnumerable<MessageMedia> entities);
    public void RemoveRange(IEnumerable<Guid> entities);
}
