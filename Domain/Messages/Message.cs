using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Messages.Internal;

namespace AlphaAuraChat.Domain.Messages;

public sealed class Message : Entity
{
    public Guid ConversationId { get; private set; }
    public Guid SenderId { get; private set; }
    public Guid? ReplyToMessageId { get; private set; }
    public Content Content { get; private set; } = default!;
    public MessageStatus Status { get; private set; } = null!;
    public DateTime SentAtUtc { get; private set; }
    public bool IsContainingMedia { get; private set; }
    private Message(Guid id, Guid conversationId, Guid senderId, Content content, DateTime sentAtUtc, bool isContainingMedia) : base(id)
    {
        ConversationId = conversationId;
        SenderId = senderId;
        Content = content;
        SentAtUtc = sentAtUtc;
        IsContainingMedia = isContainingMedia;
    }
    private Message() : base() { } // for EfCore

    public static Message Create(Guid conversationId, Guid senderId, string content, DateTime sentAtUtc, bool isContainingMedia)
    {
        return new Message(Guid.NewGuid(), conversationId, senderId, new Content(content), sentAtUtc, isContainingMedia);
    }

    public Result MarkAsRead(DateTime readTimeUtc)
    {
        if (Status.DeliveredAt is null)
            return Result.Failure(MessageErrors.ReadBeforeDelievered);

        if (readTimeUtc < Status.DeliveredAt)
            return Result.Failure(MessageErrors.ReadTimeBeforeDelieveredTime);

        Status = new MessageStatus(Status.DeliveredAt, readTimeUtc);

        return Result.Success();
    }

    public Result MarkAsDelivered(DateTime deliveredTimeUtc)
    {
        Status = new MessageStatus(deliveredTimeUtc, null);

        return Result.Success();
    }
}