using AlphaAuraChat.Domain.Abstractions;

namespace AlphaAuraChat.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static Error ExpiryMustBeGreaterThanActivation => new("Subscription.ExpiryMustBeGreaterThanActivation",
        "Expiry time must be greater than activation time.", ErrorCategory.Validation);
    public static Error AlreadyActive => new("Subscription.AlreadyActive", "Subscription is already active.", ErrorCategory.Conflict);
    public static Error AlreadyInactive => new("Subscription.AlreadyInactive", "Subscription is already inactive.", ErrorCategory.Conflict);
    public static Error AlreadyExpired => new("Subscription.AlreadyExpired", "Subscription is already expired.", ErrorCategory.Conflict);
    public static Error AlreadySuspended => new("Subscription.AlreadySuspended", "Subscription is already suspended.", ErrorCategory.Conflict);
    public static Error InvalidExpiryDate => new("Subscription.InvalidExpiryDate",
        "The new expiry date must be greater than the current expiry date.", ErrorCategory.Validation);
    public static Error CannotActivateExpiredSubscription => new("Subscription.CannotActivateExpiredSubscription",
        "Cannot activate an expired subscription.", ErrorCategory.Conflict); // not sure if this is needed, but just in case
    public static Error CannotActivateSuspendedSubscription => new("Subscription.CannotActivateSuspendedSubscription",
        "Cannot activate a suspended subscription.", ErrorCategory.Conflict);

}