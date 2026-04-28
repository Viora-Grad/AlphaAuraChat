using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Domain.Users;

public sealed class Customer : User
{
    public RelativeId RelativeId { get; private set; } = default!;
    private Customer(Guid id, Guid tenantId, RelativeId relativeId, FirstName firstName, LastName lastName) : base(id, tenantId, firstName, lastName)
    {
        RelativeId = relativeId;
    }
    private Customer() : base() { } // for EfCore


    public static Customer Create(FirstName firstName, LastName lastName, Guid tenantId, RelativeId relativeId)
    {
        return new Customer(Guid.NewGuid(), tenantId, relativeId, firstName, lastName);
    }
}
