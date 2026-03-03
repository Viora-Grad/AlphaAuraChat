namespace AlphaAuraChat.Domain.Conversations;

public interface IConversationRepository
{
    public Task<Conversation?> GetByIdAsync(Guid conversationId, CancellationToken cancellationToken);
    public Task<Conversation?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
