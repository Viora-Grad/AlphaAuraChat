using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans;

public static class PlanErrors
{
    public static Error AlreadyActive => new("Plan.AlreadyActive", "Plan is already active.");
    public static Error AlreadyInactive => new("Plan.AlreadyInactive", "Plan is already inactive.");
    public static Error AlreadySuspended => new("Plan.AlreadySuspended", "Plan is already suspended.");
    public static Error AlreadyCancelled => new("Plan.AlreadyCancelled", "Plan is already cancelled.");
    public static Error InvalidStatusTransition => new("Plan.InvalidStatusTransition", "Invalid plan status transition.");
    public static Error LimitationsMirrored => new("Plan.LimitationsMirrored", "Plan limitations cannot be the same as the current ones.");
    public static Error LimitationsExceeded => new("Plan.LimitationsExceeded", "Plan limitations exceeded.");
    public static Error LimitationsMustBeGreaterThanZero => new("Plan.LimitationsMustBeGreaterThanZero", "Plan limitations must be greater than zero.");
    public static Error InvalidLimitations => new("Plan.InvalidLimitations", "Plan limitations are invalid.");

}
