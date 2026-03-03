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
}
