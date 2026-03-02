namespace AlphaAuraChat.Application.Abstractions.FileStorage;

public class MediaUploadModel
{
    public string Name { get; init; } = null!;
    public string Type { get; init; } = null!;
    public Stream Content { get; init; } = null!;
}
