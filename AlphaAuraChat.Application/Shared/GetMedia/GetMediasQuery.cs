using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Shared.GetMedia;

public sealed record GetMediasQuery(IEnumerable<Guid> Ids) : IQuery<IEnumerable<DetailedMediaResponse>>;
