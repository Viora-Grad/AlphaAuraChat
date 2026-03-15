using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Media.Internal;

namespace AlphaAuraChat.Domain.Media;

// Mime Type is to be stored and defined in the infrastructure layer.
public class MediaFile : Entity
{
    public MediaType Type { get; private set; }
    public long SizeInBytes { get; private set; }
    public MediaLocation Path { get; private set; } = default!;
    public DateTime UploadedAtUtc { get; private set; }

    protected MediaFile(Guid id, MediaType type, long sizeInBytes, MediaLocation path, DateTime uploadedAtUtc) : base(id)
    {
        Type = type;
        SizeInBytes = sizeInBytes;
        Path = path;
        UploadedAtUtc = uploadedAtUtc;
    }

    protected MediaFile() : base() { } // for EfCore
}