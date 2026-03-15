using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Messages.GetMessages;

internal sealed record GetMessagesQuery(Guid ConversationId, int MessageCount) : IQuery<IEnumerable<MessageResponse>>;
