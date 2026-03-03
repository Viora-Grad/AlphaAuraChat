namespace AlphaAuraChat.Domain.Messages.Internal;

public record MessageStatus(DateTime? DeliveredAt, DateTime? ReadAt);