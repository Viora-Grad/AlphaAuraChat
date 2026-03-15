namespace AlphaAuraChat.Domain.Media;

public interface IMediasRepository
{
    public IQueryable<MediaFile> GetByIds(IEnumerable<Guid> Ids);
}
