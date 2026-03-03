using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.Exceptions;
using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Application.Abstractions.Realtime;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Conversations;
using AlphaAuraChat.Domain.Messages;

namespace AlphaAuraChat.Application.Messages.SendMessage;

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

        if (request.Media is not null)
            await storage.UploadAsync(request.Media, cancellationToken);

        await realTimeNotifier.SendMessageAsync(
            recieverId,
            request.ConversationId,
            request.Content,
            message.SentAtUtc,
            request.Media,
            cancellationToken);

        return Result.Success();
    }
}
