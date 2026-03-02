using AlphaAuraChat.Domain.Users.Internal;

namespace AlphaAuraChat.Domain.Users;

public sealed class TenantMember : User
{
    private readonly List<Role> _roles = [];
    public IReadOnlyCollection<Role> Roles => [.. _roles];

    private TenantMember(Guid id, Guid tenantId, FirstName firstName, LastName lastName) : base(id)
    {
        TenantId = tenantId;
        FirstName = firstName;
        LastName = lastName;
    }
    private TenantMember() : base() { } // for EfCore
}
