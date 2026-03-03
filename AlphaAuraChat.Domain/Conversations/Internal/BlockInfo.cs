namespace AlphaAuraChat.Domain.Conversations.Internal;

public record BlockInfo(bool IsBlocked, DateTime BlockDate, Guid BlockedBy);
