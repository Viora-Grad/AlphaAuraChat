using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Messages;

public static class MessageErrors
{
    public static Error ReadBeforeDelievered =>
        new("Message.ReadBeforeDelievered", "Message must be marked as delivered before marking it as read.", ErrorCategory.Conflict);

    public static Error ReadTimeBeforeDelieveredTime
        => new("Message.ReadTimeBeforeDelieveredTime", "Delivery time must be before read time.", ErrorCategory.Conflict);
}
