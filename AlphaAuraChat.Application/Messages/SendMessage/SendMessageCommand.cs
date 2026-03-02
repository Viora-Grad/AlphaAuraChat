using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Messages.SendMessage;

public record SendMessageCommand(Guid ConversationId, Guid SenderId, string Content, IEnumerable<MediaUploadModel>? Media) : ICommand;