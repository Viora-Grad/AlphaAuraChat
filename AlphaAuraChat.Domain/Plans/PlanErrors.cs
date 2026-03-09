using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Plans;

public static class PlanErrors
{
    public static Error AlreadyActive => new("Plan.AlreadyActive", "Plan is already active.", ErrorCategory.Conflict);
    public static Error AlreadyInactive => new("Plan.AlreadyInactive", "Plan is already inactive.", ErrorCategory.Conflict);
    public static Error AlreadySuspended => new("Plan.AlreadySuspended", "Plan is already suspended.", ErrorCategory.Conflict);
    public static Error AlreadyCancelled => new("Plan.AlreadyCancelled", "Plan is already cancelled.", ErrorCategory.Conflict);
    public static Error InvalidStatusTransition => new("Plan.InvalidStatusTransition", "Invalid plan status transition.", ErrorCategory.Violation);
    public static Error LimitationsMirrored => new("Plan.LimitationsMirrored", "Plan limitations cannot be the same as the current ones.", ErrorCategory.Violation);
    public static Error LimitationsExceeded => new("Plan.LimitationsExceeded", "Plan limitations exceeded.", ErrorCategory.Validation); // no longer needed 
    public static Error LimitationsMustBeGreaterThanZero => new("Plan.LimitationsMustBeGreaterThanZero", "Plan limitations must be greater than zero.", ErrorCategory.Validation);
    public static Error InvalidLimitations => new("Plan.InvalidLimitations", "Plan limitations are invalid.", ErrorCategory.Validation);
    public static Error SimilarPlanExists => new("Plan.SimilarPlanExists", "A similar plan already exists.", ErrorCategory.Validation);
}
