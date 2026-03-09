using AlphaAuraChat.Application.Abstractions.Caching;

namespace AlphaAuraChat.Application.Plans.GetPlan;

public sealed record GetPlanQuery(Guid PlanId) : ICachedQuery<PlanResponse>
{
    public string CacheKey => $"plan:{PlanId}";
    public TimeSpan? Expiration => TimeSpan.FromHours(1); // Cache plan details for 1 hour, can be adjusted based on expected update frequency and performance needs.
}
