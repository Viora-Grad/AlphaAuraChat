using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants.Internal;

namespace AlphaAuraChat.Domain.Tenants;

public sealed class Tenant : Entity
{
    public Name Name { get; private set; } = null!;
    public Contact Contacts { get; private set; } = null!;
    public PrivateKey PrivateKey { get; private set; } = null!;
    public Subscription Subscription { get; private set; } = null!;
    private Tenant(Guid id, Name name, Contact contacts, PrivateKey privateKey, Subscription subscription) : base(id)
    {
        Name = name;
        Contacts = contacts;
        PrivateKey = privateKey;
        Subscription = subscription;
    }
    private Tenant() : base() { } // for EfCore

    public Result Activate()
    {
        if (Subscription.Status == Status.Active)
        {
            Result.Failure(TenantErrors.AlreadyActivate);
        }
        Subscription = Subscription with { Status = Status.Active };
        return Result.Success();
    }
    public Result ChangeSubscription(Guid planId)
    {
        if (Subscription.PlanId == planId)
        {
            return Result.Failure(TenantErrors.SamePlan);
        }
        Subscription = Subscription with { PlanId = planId };
        return Result.Success();
    }

    public Result RenewSubscription(TimeSpan duration, DateTime activationTimeUtc)
    {
        var newExpiryTimeUtc = activationTimeUtc.Add(duration);
        if (newExpiryTimeUtc <= activationTimeUtc)
        {
            return Result.Failure(TenantErrors.InvalidDuration);
        }
        Subscription = Subscription with { ExpiryTimeUtc = newExpiryTimeUtc, Status = Status.Active };
        return Result.Success();
    }
}
