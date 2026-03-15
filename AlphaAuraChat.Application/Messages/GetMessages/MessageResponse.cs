using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Application.Messages.GetMessages;

public class MessageResponse
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }

    public string Content { get; set; } = null!;
    public Guid? ReplayToMessageId { get; set; }
    public string? ReplyToMessageContent { get; set; }

    public DateTime SentAtUtc { get; set; }
    public DateTime? RecievedAtUtc { get; set; }
    public DateTime? ReadAtUtc { get; set; }

    public IEnumerable<PartialMediaResponse> Medias { get; set; } = default!;
}

public class PartialMediaResponse
{
    public Guid Id { get; set; }
    public long SizeInBytes { get; set; }
    public MediaType Type { get; set; }
}