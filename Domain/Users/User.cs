using AlphaAuraChat.Domain.Abstractions;
using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Domain.Users;

public abstract class User : Entity
{
    public Guid TenantId { get; protected set; }
    public FirstName FirstName { get; protected set; } = null!;
    public LastName LastName { get; protected set; } = null!;

    protected User(Guid id, Guid tenantId, FirstName firstName, LastName lastName) : base(id)
    {
        TenantId = tenantId;
        FirstName = firstName;
        LastName = lastName;
    }
    protected User(Guid id) : base(id) { } // for creating a user with only an ID, useful for certain operations like deletion or referencing without loading the entire entity.
    protected User() : base() { } // for EfCore
}
