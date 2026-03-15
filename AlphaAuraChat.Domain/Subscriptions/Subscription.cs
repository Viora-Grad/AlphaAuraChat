
using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Subscriptions.Internal;

namespace AlphaAuraChat.Domain.Subscriptions;

public class Subscription : Entity
{
    public Guid PlanId { get; private set; }
    public Guid ClientId { get; private set; }
    public DateTime ActiviationTimeUtc { get; private set; }
    public DateTime ExpiryTimeUtc { get; private set; }
    public Status Status { get; private set; }
    private Subscription(Guid id, Guid planId, Guid clientId, DateTime activiationTimeUtc, DateTime expiryTimeUtc) : base(id)
    {
        PlanId = planId;
        ClientId = clientId;
        ActiviationTimeUtc = activiationTimeUtc;
        ExpiryTimeUtc = expiryTimeUtc;
        Status = Status.Inactive;
    }
    private Subscription() : base() { } // for EfCore

    public static Result<Subscription> Create(Guid planId, Guid clientId, DateTime activiationTimeUtc, DateTime expiryTimeUtc)
    {
        if (expiryTimeUtc <= activiationTimeUtc)
            return Result.Failure<Subscription>(SubscriptionErrors.ExpiryMustBeGreaterThanActivation);

        var subscription = new Subscription(Guid.NewGuid(), planId, clientId, activiationTimeUtc, expiryTimeUtc);
        return Result.Success(subscription);
    }
    public Result Activate()
    {
        if (Status == Status.Active)
            return Result.Failure(SubscriptionErrors.AlreadyActive);
        if (Status == Status.Suspended)
            return Result.Failure(SubscriptionErrors.CannotActivateSuspendedSubscription);
        Status = Status.Active;
        return Result.Success();
    }
    public Result Deactivate()
    {
        if (Status == Status.Inactive)
            return Result.Failure(SubscriptionErrors.AlreadyInactive);
        Status = Status.Inactive;
        return Result.Success();
    }
    public Result Expire()
    {
        if (Status == Status.Expired)
            return Result.Failure(SubscriptionErrors.AlreadyExpired);
        Status = Status.Expired;
        return Result.Success();
    }
    public Result Suspend()
    {
        if (Status == Status.Suspended)
            return Result.Failure(SubscriptionErrors.AlreadySuspended);
        Status = Status.Suspended;
        return Result.Success();

    }
    public Result RenewSubscription(DateTime newExpiryTimeUtc)
    {
        if (newExpiryTimeUtc <= ExpiryTimeUtc)
            return Result.Failure(SubscriptionErrors.InvalidExpiryDate);
        ExpiryTimeUtc = newExpiryTimeUtc;
        return Result.Success();
    }

}
