namespace AlphaAuraChat.Domain.Messages;

public interface IMessageMediasRepository
{
    public Task<MessageMedia?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public IQueryable<MessageMedia> GetByIds(IEnumerable<Guid> ids);
    public void AddRange(IEnumerable<MessageMedia> entities);
    public void RemoveRange(IEnumerable<Guid> entities);
}
