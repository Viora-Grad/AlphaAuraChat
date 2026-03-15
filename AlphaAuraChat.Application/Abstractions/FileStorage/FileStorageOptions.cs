namespace AlphaAuraChat.Application.Abstractions.FileStorage;

internal sealed record FileStorageOptions
{
    public string StoragePath { get; init; } = null!;
}
