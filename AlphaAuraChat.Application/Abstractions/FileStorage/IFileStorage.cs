using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Application.Abstractions.FileStorage;

public interface IFileStorage
{
    Task<Result<string>> UploadAsync(
        MediaUploadModel media,
        CancellationToken cancellationToken = default);

    Task<Result<List<string>>> UploadAsync(
        IEnumerable<MediaUploadModel> media,
        CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(
        string path,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default);
}
