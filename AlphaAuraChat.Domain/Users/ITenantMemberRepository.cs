namespace AlphaAuraChat.Domain.Users;

public interface ITenantMemberRepository
{
    Task<TenantMember> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(TenantMember tenantMember);
}
