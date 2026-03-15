using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Media;
using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Domain.Messages;

public sealed class MessageMedia : MediaFile
{
    public Guid MessageId { get; set; }

    private MessageMedia(Guid id, Guid messageId, MediaType type, long sizeInBytes, MediaLocation location, DateTime uploadedAtUtc)
        : base(id, type, sizeInBytes, location, uploadedAtUtc)
    {
        MessageId = messageId;
    }

    public static Result<MessageMedia> Create(Guid messageId, MediaType type, long sizeInBytes, string name, DateTime uploadedAtUtc)
    {
        if (sizeInBytes <= MediaRules.MaxSizeInBytes)
            return Result.Failure<MessageMedia>(MediaErrors.MediaSizeExceedsLimit);

        var guid = Guid.NewGuid();
        return Result.Success(new MessageMedia(guid, messageId, type, sizeInBytes, new MediaLocation($"{guid}-{name}"), uploadedAtUtc));
    }
}
