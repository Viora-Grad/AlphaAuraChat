using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Domain.Media;

// Mime Type is to be stored and defined in the infrastructure layer.
public sealed class MessageMedia : Entity
{
    public Guid MessageId { get; private set; }
    public MediaType Type { get; private set; }
    public long SizeInBytes { get; private set; }
    public MediaLocation Path { get; private set; } = default!;

    private MessageMedia(Guid id, Guid messageId, MediaType type, long sizeInBytes, MediaLocation path) : base(id)
    {
        MessageId = messageId;
        Type = type;
        SizeInBytes = sizeInBytes;
        Path = path;
    }

    private MessageMedia() : base() { } // for EfCore

    public static Result<MessageMedia> Create(Guid messageId, MediaType type, long sizeInBytes, string name)
    {
        if (sizeInBytes <= MediaRules.MaxSizeInBytes)
            return Result.Failure<MessageMedia>(MediaErrors.MediaSizeExceedsLimit);

        var guid = Guid.NewGuid();
        return Result.Success(new MessageMedia(guid, messageId, type, sizeInBytes, new MediaLocation($"{guid}-{name}")));
    }
}