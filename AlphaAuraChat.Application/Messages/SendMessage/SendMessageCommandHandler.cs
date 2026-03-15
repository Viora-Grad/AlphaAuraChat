using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Application.Abstractions.Realtime;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Conversations;
using AlphaAuraChat.Domain.Messages;
using Microsoft.Extensions.Options;

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
    IMessageMediasRepository mediaRepository,
    IDateTimeProvider dateTimeProvider,
    IFileStorage storage,
    IRealtimeNotifier realTimeNotifier,
    IOptions<FileStorageOptions> storageOptions,
    IUnitOfWork unitOfWork) : ICommandHandler<SendMessageCommand>
{
    public async Task<Result> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(
            request.ConversationId,
            request.SenderId,
            request.Content,
            dateTimeProvider.UtcNow,
            request.Medias is not null && request.Medias.Any());

        var conversation = await conversationRepository.GetByIdAsync(request.ConversationId, cancellationToken) ??
            throw new NotFoundException($"Conversation with id {request.ConversationId} does not exist");

        var recieverId = conversation.ClientId == request.SenderId ? conversation.CustomerId : conversation.ClientId;

        messagesRepository.Add(message);

        var mediaSaveResult = await SaveAndUploadMedia(request.Medias, message.Id, cancellationToken);

        if (mediaSaveResult.IsFailure)
            return mediaSaveResult;

        await realTimeNotifier.SendMessageAsync(
            recieverId,
            request.ConversationId,
            request.Content,
            message.SentAtUtc,
            request.Medias,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    private async Task<Result> SaveAndUploadMedia(IEnumerable<MediaUploadModel>? mediaUpload, Guid messageId, CancellationToken cancellationToken)
    {
        // short hand if media exists or not if no media then return success otherwise continue with media upload
        if (mediaUpload is null || mediaUpload.Any() == false)
            return Result.Success();

        List<MessageMedia> messageMedias = [];
        foreach (var media in mediaUpload)
        {
            var mediaResult = MessageMedia
                .Create(messageId, MimeToMediaTypeConvertor.Convert(media.Type), media.Content.Length, media.Name, dateTimeProvider.UtcNow);

            if (mediaResult.IsFailure)
                return Result.Failure(mediaResult.Error);

            messageMedias.Add(mediaResult.Value);
        }

        mediaRepository.AddRange(messageMedias);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        List<Guid> failedMediaIds = [];

        foreach (var (media, entity) in mediaUpload.Zip(messageMedias))
        {
            var path = $"{storageOptions.Value.StoragePath}/{entity.Id}-{media.Name}";

            var result = await storage.UploadAsync(media.Content, path, cancellationToken);

            if (result.IsFailure)
                failedMediaIds.Add(entity.Id);
        }

        // reroll the media upload if any of the uploads have failed and remove the failed media entities from the database to maintain consistency
        mediaRepository.RemoveRange(failedMediaIds);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
