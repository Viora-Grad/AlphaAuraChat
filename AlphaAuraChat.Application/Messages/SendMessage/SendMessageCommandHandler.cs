using AlphaAuraChat.Application.Abstractions.Clock;
using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;
using AlphaAuraChat.Application.Abstractions.Realtime;
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Messages;

namespace AlphaAuraChat.Application.Messages.SendMessage;

internal sealed class SendMessageCommandHandler(
    IMessagesRepository messagesRepository,
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

        messagesRepository.Add(message);

        if (request.Media is not null)
            await storage.UploadAsync(request.Media, cancellationToken);

        return Result.Success();
    }
}
