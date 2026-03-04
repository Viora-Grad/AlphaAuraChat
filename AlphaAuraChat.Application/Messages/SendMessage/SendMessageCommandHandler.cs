using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Application.Abstractions.Realtime;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Conversations;
using AlphaAuraChat.Domain.Messages;

namespace AlphaAuraChat.Application.Messages.SendMessage;

/// <summary>
/// Handles the processing of send message commands, including message creation, storage, and real-time notification
/// within a conversation.
/// </summary>
/// <exception cref="NotFoundException"></exception>
/// <remarks>This handler ensures that messages are created and stored reliably, and that recipients are notified
/// in real time. It also manages media uploads when messages include attachments </remarks>
internal sealed class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
    IConversationRepository conversationRepository,
    IDateTimeProvider dateTimeProvider,
    IFileStorage storage,
    IRealtimeNotifier realTimeNotifier) : ICommandHandler<SendMessageCommand>
{
    public async Task<Result> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(
            request.ConversationId,
            request.SenderId,
            request.Content,
            dateTimeProvider.UtcNow,
            request.Media is not null && request.Media.Any());

        var conversation = await conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken) ??
            throw new NotFoundException($"Conversation with id {request.ConversationId} does not exist");

        var recieverId = conversation.ClientId == request.SenderId ? conversation.CustomerId : conversation.ClientId;

        messagesRepository.Add(message);

        await SaveAndUploadMedia(request.Media, cancellationToken);

        await realTimeNotifier.SendMessageAsync(
            recieverId,
            request.ConversationId,
            request.Content,
            message.SentAtUtc,
            request.Media,
            cancellationToken);

        return Result.Success();
    }

    private async Task SaveAndUploadMedia(IEnumerable<MediaUploadModel>? media, CancellationToken cancellationToken)
    {
        if (media == null || media.Any() == false)
            return;

        await storage.UploadAsync(media, cancellationToken);


    }
}
