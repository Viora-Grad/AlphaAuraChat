namespace AlphaAuraChat.Domain.Media;

public interface IMediaRepository
{
    public void AddRange(IEnumerable<MessageMedia> entities);
    public void RemoveRange(IEnumerable<Guid> entities);
}
