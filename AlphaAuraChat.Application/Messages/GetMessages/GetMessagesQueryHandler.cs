using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Application.Messages.GetMessages;

internal sealed class GetMessagesQueryHandler : IQueryHandler<GetMessagesQuery, IEnumerable<MessageResponse>>
{
    public Task<Result<IEnumerable<MessageResponse>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        // look at cursor pagination first broddy, then look at offset pagination if cursor pagination is not possible
        throw new NotImplementedException();
    }
}
