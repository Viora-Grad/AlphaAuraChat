using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Application.Abstractions.FileStorage;

internal static class MimeToMediaTypeConvertor
{
    public static MediaType Convert(string mimeType)
    {
        return mimeType switch
        {
            "image/jpeg" => MediaType.Image,
            "image/png" => MediaType.Image,
            "image/gif" => MediaType.Image,

            "video/mp4" => MediaType.Video,
            "video/mpeg" => MediaType.Video,
            "video/quicktime" => MediaType.Video,

            "audio/mpeg" => MediaType.Audio,
            "audio/wav" => MediaType.Audio,

            "application/pdf" => MediaType.Document,

            _ => throw new NotSupportedException($"MIME type {mimeType} is not supported")
        };
    }
}