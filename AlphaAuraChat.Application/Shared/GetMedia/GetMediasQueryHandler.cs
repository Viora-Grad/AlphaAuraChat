using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Media;

namespace AlphaAuraChat.Application.Shared.GetMedia;

internal sealed class GetMediasQueryHandler(
    IMediasRepository mediaRepository
    ) : IQueryHandler<GetMediasQuery, IEnumerable<DetailedMediaResponse>>
{
    public Task<Result<IEnumerable<DetailedMediaResponse>>> Handle(GetMediasQuery request, CancellationToken cancellationToken)
    {
        var medias = mediaRepository.GetByIds(request.Ids);

        IEnumerable<DetailedMediaResponse> medias = mediaRepository.GetByIds(request.Ids)
            .Select(m => new DetailedMediaResponse(m.Id, m.FileType, m.SizeInBytes, m.UploadedAtUtc));
    }
}
