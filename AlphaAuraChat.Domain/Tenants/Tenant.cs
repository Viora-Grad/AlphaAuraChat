using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants.Internal;
using AlphaAuraChat.Domain.Users;

namespace AlphaAuraChat.Domain.Tenants;

public sealed class Tenant : Entity
{
    public Name Name { get; private set; } = null!;
    public Contact Contacts { get; private set; } = null!;
    public PrivateKey PrivateKey { get; private set; } = null!;
    public Subscription Subscription { get; private set; } = null!;
    public User TenantOwner { get; private set; } = null!;
    private Tenant(Guid id, Name name, Contact contacts, PrivateKey privateKey, Subscription subscription, User tenantOwner) : base(id)
    {
        Name = name;
        Contacts = contacts;
        PrivateKey = privateKey;
        Subscription = subscription;
        TenantOwner = tenantOwner;
    }
    private Tenant() : base() { } // for EfCore


    public static Tenant Create(Name name, Contact contact, User onwer, string key, DateTime activationTimeUtc)
    {
        var tenant = new Tenant(
            Guid.NewGuid(),
            name,
            contact,
            new PrivateKey(key, activationTimeUtc, 1),
            Subscription.CreateDefaultSubscription(),
            onwer);
        return tenant;

    }
    public Result Activate()
    {
        if (Subscription.Status == Status.Active)
            Result.Failure(TenantErrors.AlreadyActivate);
        Subscription = Subscription with { Status = Status.Active };
        return Result.Success();
    }
    public Result ChangeSubscription(Guid planId)
    {
        if (Subscription.PlanId == planId)
            return Result.Failure(TenantErrors.SamePlan);
        Subscription = Subscription with { PlanId = planId };
        return Result.Success();
    }

    public Result RenewSubscription(TimeSpan duration, DateTime activationTimeUtc)
    {
        var newExpiryTimeUtc = activationTimeUtc.Add(duration);
        if (newExpiryTimeUtc <= activationTimeUtc)
            return Result.Failure(TenantErrors.InvalidDuration);
        Subscription = Subscription with { ExpiryTimeUtc = newExpiryTimeUtc, Status = Status.Active };
        return Result.Success();
    }
}
