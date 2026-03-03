using AlphaAuraChat.Application.Abstractions.FileStorage;

namespace AlphaAuraChat.Application.Abstractions.Realtime;

public interface IRealtimeNotifier
{
    public Task SendMessageAsync(
        Guid recieverId,
        Guid conversationId,
        string content,
        DateTime sentAtUtc,
        IEnumerable<MediaUploadModel>? media,
        CancellationToken cancellationToken);
}
