using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Media.Internal;

public static class MediaErrors
{
    public static Error MediaSizeExceedsLimit => new("Message.MediaSizeExceedsLimit", "The size of the media exceeds the allowed limit.");
}
