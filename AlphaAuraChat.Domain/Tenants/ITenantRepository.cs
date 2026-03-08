namespace AlphaAuraChat.Domain.Tenants;

public interface ITenantRepository
{
    public Task<Tenant?> GetByIdAsync(Guid tenantId, CancellationToken cancellationToken);
    public void Add(Tenant tenant);
}
