using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Tenants.Events;
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

    public void Activate()
    {
        if (Subscription.Status == Status.Active)
        {
            return;
        }
        Subscription = Subscription with { Status = Status.Active };
        RaiseDomainEvent(new TenantActivatedDomainEvent(Id));
    }
    public void ChangeSubscription(Guid planId)
    {
        if (Subscription.PlanId == planId)
        {
            return;
        }
        Subscription = Subscription with { PlanId = planId };
        RaiseDomainEvent(new TenantSubscriptionChangedDomainEvent(Id));
    }

    public void RenewSubscription(TimeSpan duration, DateTime activationTimeUtc)
    {
        var newExpiryTimeUtc = activationTimeUtc.Add(duration);
        if (Subscription.ExpiryTimeUtc > activationTimeUtc)
        {
            newExpiryTimeUtc = Subscription.ExpiryTimeUtc.Add(duration);
        }
        Subscription = Subscription with { ExpiryTimeUtc = newExpiryTimeUtc };
        RaiseDomainEvent(new TenantSubscriptionRenewedDomainEvent(Id));

    }
}
