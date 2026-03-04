using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans.Internal;

public record Limitations(int MaximumAdmins, int MaximumSupervisors, int MaximumAgents, int MaximumConversations)
{
    public static Result<Limitations> Validate(Limitations limitations)
    {
        if (limitations.MaximumAdmins < 0 ||
            limitations.MaximumSupervisors < 0 ||
            limitations.MaximumAgents < 0 ||
            limitations.MaximumConversations < 0)
        {
            return Result.Failure<Limitations>(PlanErrors.LimitationsMustBeGreaterThanZero);
        }

        return Result.Success(limitations);
    }

    public bool Exceeds(Limitations other) =>
        MaximumAdmins > other.MaximumAdmins
        || MaximumSupervisors > other.MaximumSupervisors
        || MaximumAgents > other.MaximumAgents
        || MaximumConversations > other.MaximumConversations;
}