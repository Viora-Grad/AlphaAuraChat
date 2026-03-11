using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Application.Abstractions.FileStorage;

public interface IFileStorage
{
    Task<Result> UploadAsync(
        Stream media,
        string path,
        CancellationToken cancellationToken = default);

    Task<Stream> DownloadAsync(
        string path,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default);
}
