using AlphaAuraChat.Application.Abstractions.Clock;

namespace AlphaAuraChat.Infrastructure.Clock;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
