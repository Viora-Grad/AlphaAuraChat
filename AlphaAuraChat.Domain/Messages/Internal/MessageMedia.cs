using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Messages.Internal;

// Mime Type is to be stored and defined in the infrastructure layer.
public sealed class MessageMedia : Entity
{
    public Guid MessageId { get; private set; }
    public MediaTypes Type { get; private set; }
    public long SizeInBytes { get; private set; }

    private MessageMedia(Guid id, Guid messageId, MediaTypes type, long sizeInBytes) : base(id)
    {
        MessageId = messageId;
        Type = type;
        SizeInBytes = sizeInBytes;
    }

    private MessageMedia() : base() { } // for EfCore

    public static Result<MessageMedia> Create(Guid messageId, MediaTypes type, long sizeInBytes, long maxSizeInBytes)
    {
        if (sizeInBytes <= maxSizeInBytes)
            return Result.Failure<MessageMedia>(MessageErrors.MediaSizeExceedsLimit);

        return Result.Success(new MessageMedia(Guid.NewGuid(), messageId, type, sizeInBytes));
    }
}