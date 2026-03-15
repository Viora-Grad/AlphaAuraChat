namespace AlphaAuraChat.Domain.Subscriptions.Internal;

public enum Status
{
    Active,
    Inactive,
    Expired,
    CancelledAtPeriodEnd, // subscription is active until the end of the current billing period, but will not renew after that
    Canceled, // immediate cancellation
    Suspended
}