using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Domain.Media;

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

    public static Result<MessageMedia> Create(Guid messageId, MediaTypes type, long sizeInBytes)
    {
        if (sizeInBytes <= MediaRules.MaxSizeInBytes)
            return Result.Failure<MessageMedia>(MediaErrors.MediaSizeExceedsLimit);

        return Result.Success(new MessageMedia(Guid.NewGuid(), messageId, type, sizeInBytes));
    }
}