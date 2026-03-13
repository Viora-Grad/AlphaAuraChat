namespace AlphaAuraChat.Domain.Users;

public interface ITenantMemberRepository
{
    void Add(TenantMember tenantMember);
}
