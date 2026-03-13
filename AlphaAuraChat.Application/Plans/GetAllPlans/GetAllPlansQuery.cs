using AlphaAuraChat.Application.Abstractions.Caching;

namespace AlphaAuraChat.Application.Plans.GetAllPlans;

public sealed record GetAllPlansQuery() : ICachedQuery<IReadOnlyList<PlansResponse>>
{
    public string CacheKey => "plans:all";
    public TimeSpan? Expiration => TimeSpan.FromHours(1); // Cache the list of plans for 1 hour, can be adjusted based on expected update frequency and performance needs.
}
