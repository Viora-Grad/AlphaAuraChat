namespace AlphaAuraChat.Application.Shared.GetMedia;

public class DetailedMediaResponse(Guid id, string name, string mimeType, long sizeInBytes)
{
    public Guid Id { get; init; } = id;
    public Stream Content { get; set; }
    public string Name { get; init; } = name;
    public string MimeType { get; init; } = mimeType;
    public long SizeInBytes { get; init; } = sizeInBytes;
}
