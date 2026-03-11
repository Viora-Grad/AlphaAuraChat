using AlphaAuraChat.Application.Abstractions.FileStorage;
using AlphaAuraChat.Application.Abstractions.Messaging;

namespace AlphaAuraChat.Application.Messages.SendMessage;

/// <summary>
/// Sends and saves the message.
/// </summary>
public sealed record SendMessageCommand(Guid ConversationId, Guid SenderId, string Content, IEnumerable<MediaUploadModel>? Media) : ICommand;